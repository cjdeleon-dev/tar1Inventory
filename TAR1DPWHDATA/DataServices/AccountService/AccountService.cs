using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAR1DPWHDATA.DataAccesses.AccountAccess;
using TAR1DPWHDATA.DataModels;

namespace TAR1DPWHDATA.DataServices.AccountService
{
    public class AccountService : IAccountService
    {
        IAccountAccess iaaccess;
        public AccountService()
        {
            this.iaaccess = new AccountAccess();
        }

        public bool DeletePhoto(int id)
        {
            return iaaccess.DeletePhoto(id);
        }

        public string GetPasswordByID(int id)
        {
            return iaaccess.GetPasswordByID(id);
        }

        public ProcessViewModel UpdatePasswordByID(int id, string password)
        {
            return iaaccess.UpdatePasswordByID(id, password);
        }

        public bool UpdatePhoto(int Id, byte[] ImageByte)
        {
            return iaaccess.UpdatePhoto(Id, ImageByte);
        }
    }
}