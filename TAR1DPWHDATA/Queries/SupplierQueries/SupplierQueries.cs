using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.SupplierQueries
{
    public class SupplierQueries
    {
        public static readonly string sqlGetAllSuppliers = "select id, name, address from suppliers;";
        public static readonly string sqlInsertSupplier = "insert into suppliers(name,address,createddate,createdbyid) " +
                                                          "values(@supplier,@address,getdate(),@createdbyid)";
        public static readonly string sqlUpdateSupplier = "update suppliers set name=@supplier,address=@address," +
                                                          "updatedbyid=@updatedbyid,lastupdated=getdate() " +
                                                          "where id=@id;";
        public static readonly string sqlRemoveSupplier = "delete from suppliers where id=@id;";
    }
}