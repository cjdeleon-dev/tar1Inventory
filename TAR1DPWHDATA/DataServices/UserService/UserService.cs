using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.UserAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.UserService
{
    public class UserService : IUserService
    {
        IUserAccess iuaccess;

        public UserService()
        {
            this.iuaccess = new UserAccess();
        }

        public ProcessViewModel ChangeUserCredentials(UserModel user)
        {
            return iuaccess.ChangeUserCredentials(user);
        }

        public ProcessViewModel DeactivateUser(int id)
        {
            return iuaccess.DeactivateUser(id);
        }

        public UserViewModel GetAllUsers()
        {
            return iuaccess.GetAllUsers();
        }

        public ProcessViewModel InsertUser(UserModel user)
        {
            return iuaccess.InsertUser(user);
        }

        public ProcessViewModel UpdateUser(UserModel user)
        {
            return iuaccess.UpdateUser(user);
        }
    }
}