﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common;

namespace DAL
{
    public class UserOps
    {
        public UserInfo CreateNewUser(UserInfo userInfo, UserCredentials userCredentials)
        {
            try
            {
                user tmpUser = Converter.toUser(userInfo, userCredentials);

                using (var db = new qnhdb())
                {
                    db.users.Add(tmpUser);
                    db.SaveChanges();
                }
                return Converter.toExtUser(tmpUser, out userCredentials);
            }
            catch (DbUpdateException due)
            {
                UpdateException uex = due.InnerException as UpdateException;
                if (uex != null)
                {

                    SqlException sex = uex.InnerException as SqlException;
                    if (sex != null && sex.Errors.OfType<SqlError>().Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
                    {
                        throw new DAL.Exceptions.DALDuplicateKeyError("Duplicate key error");
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public bool TryLogin(UserCredentials userCredentials, out UserInfo userInfo)
        {
            user tmpUser;
            UserInfo result = new UserInfo();

            using (var db = new qnhdb())
            {
                tmpUser = db.users.Where(x => x.name == userCredentials.Username && x.pwd == userCredentials.Password).SingleOrDefault<user>();
            }
            userInfo = null;

            if (tmpUser != null)
            {
                userInfo = Converter.toExtUser(tmpUser, out userCredentials);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
            List<UserInfo> result = new List<UserInfo>();
            IEnumerable<user> tmpUsers;
            using (var db = new qnhdb())
            {
                tmpUsers = db.users.OrderBy(x => x.name).ToList<user>();
            }

            foreach (user u in tmpUsers)
            {
                UserInfo tmpUserInfo;
                UserCredentials tmpUserCredentials;
                tmpUserInfo = Converter.toExtUser(u, out tmpUserCredentials);
                result.Add(tmpUserInfo);
            }

            
            return result;
        }

        public void SaveUserData(UserInfo userinfo)
        {
            //get the existing one from the db, change it and save back
            using (var db = new qnhdb())
            {
                user tmpUser; 
                tmpUser = db.users.Where(x => x.id == userinfo.ID).Single();

                tmpUser.ismuted = Convert.ToByte(userinfo.IsMuted);
                tmpUser.isadmin = Convert.ToByte(userinfo.IsAdmin);
                db.SaveChanges();
            }
            
        }

        public void SaveUsersData(IEnumerable<UserInfo> usersInfo)
        {
            foreach (UserInfo userInfo in usersInfo)
            {
                SaveUserData(userInfo);
            }
        }

        public UserInfo GetUserInfo(int userId)
        {
            user tmpUser = new user();

            using (var db = new qnhdb())
            {
                tmpUser = db.users.Where(x => x.id == userId).Single();
            }
            
            UserInfo tmpUserInfo;
            UserCredentials tmpUserCredentials;
            tmpUserInfo = Converter.toExtUser(tmpUser, out tmpUserCredentials);
                
            return tmpUserInfo;
        }
    }
}
