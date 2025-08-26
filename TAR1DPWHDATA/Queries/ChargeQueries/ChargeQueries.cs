using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAR1DPWHDATA.Queries.ChargeQueries
{
    public class ChargeQueries
    {
        public static readonly string sqlGetAllChargedMaterialHeaders = "select hdr.Id, CONVERT(VARCHAR,hdr.PostedDate,101)[PostedDate], hdr.PostedById, UPPER(usr.FirstName + ' ' + usr.MiddleInitial + ' ' + usr.LastName) [PostedBy],     " +
                                                                        "posPosted.[Description] [PosPostedBy], hdr.IssuedById, empIssued.[Name] [IssuedBy], posIssued.[Description] [PosIssuedBy]," +
                                                                        "hdr.ReceivedById, empReceived.[Name] [ReceivedBy], posReceived.[Description] [PosReceivedBy]," +
                                                                        "hdr.CheckedById, empChecked.[Name] [CheckedBy], posChecked.[Description] [PosCheckedBy]," +
                                                                        "hdr.AuditedById, empAudited.[Name] [AuditedBy], posAudited.[Description] [PosAuditedBy]," +
                                                                        "hdr.NotedById, empNoted.[Name] [NotedBy], posNoted.[Description] [PosNotedBy]," +
                                                                        "hdr.JOWOMOId,jwm.Code,hdr.JOWOMONumber,hdr.Project,hdr.ProjectAddress,isconsumerreceived,receivedconsumer " +
                                                                        "from ChargedMaterialHeaders hdr " +
                                                                        "left join Users usr " +
                                                                        "on hdr.PostedById=usr.Id " +
                                                                        "left join Positions posPosted " +
                                                                        "on usr.PositionId=posPosted.Id " +
                                                                        "left join Employees empIssued " +
                                                                        "on hdr.IssuedById=empIssued.Id " +
                                                                        "left join Positions posIssued " +
                                                                        "on empIssued.PositionId=posIssued.Id " +
                                                                        "left join Employees empReceived " +
                                                                        "on hdr.ReceivedById=empReceived.Id " +
                                                                        "left join Positions posReceived " +
                                                                        "on empReceived.PositionId=posReceived.Id " +
                                                                        "left join Employees empChecked " +
                                                                        "on hdr.CheckedById=empChecked.Id " +
                                                                        "left join Positions posChecked " +
                                                                        "on empChecked.PositionId=posChecked.Id " +
                                                                        "left join Employees empAudited " +
                                                                        "on hdr.AuditedById=empAudited.Id " +
                                                                        "left join Positions posAudited " +
                                                                        "on empAudited.PositionId=posAudited.Id " +
                                                                        "left join Employees empNoted " +
                                                                        "on hdr.NotedById=empNoted.Id " +
                                                                        "left join Positions posNoted " +
                                                                        "on empNoted.PositionId=posNoted.Id " +
                                                                        "left  join JOWOMO jwm " +
                                                                        "on hdr.JOWOMOId=jwm.Id Order by Id DESC;";

        public static readonly string sqlInsertChargedMaterialHeader = "insert into chargedmaterialheaders(posteddate,postedbyid,issuedbyid,receivedbyid," +
                                                                       "checkedbyid,auditedbyid,notedbyid,project,projectaddress,jowomoid,jowomonumber,isconsumerreceived,receivedconsumer) " +
                                                                       "values(@posteddate,@postedbyid,@issuedbyid,@receivedbyid," +
                                                                       "@checkedbyid,@auditedbyid,@notedbyid,@project,@projectaddress,@jowomoid,@jowomonumber,@isconsumerreceived,@receivedconsumer);";

        public static readonly string sqlGetCurrentCMHIdByUserId = "select max(Id) Id from chargedmaterialheaders where postedbyid=@postedbyid;";

        public static readonly string sqlInsertChargedMaterialDetail = "insert into ChargedMaterialDetails(chargedmaterialheaderid,materialid,serialno,quantity,unitid,jowomoid,jowomonumber) " +
                                                                        "values(@chargedmaterialheaderid,@materialid,@serialno,@quantity,@unitid,@jowomoid,@jowomonumber); " +
                                                                        "update materials set OnHand=OnHand-@quantity where id=@materialid;";

        public static readonly string sqlGetChargedMaterialDetailsByHeaderId = "select dtl.id, chargedmaterialheaderid,materialid,mat.Description [material],serialno," +
                                                                               "dtl.quantity ,dtl.unitid,unt.code [unit], dtl.JOWOMOId, jwm.Code, dtl.JOWOMONumber " +
                                                                               "from chargedmaterialdetails dtl inner join materials mat " +
                                                                               "on dtl.materialid=mat.id " +
                                                                               "inner join units unt on dtl.unitid= unt.id " +
                                                                               "inner join JOWOMO jwm on dtl.JOWOMOId=jwm.Id " +
                                                                               "where dtl.chargedmaterialheaderid= @cmhdrid;";

        public static readonly string sqlGetUnitAndOnHandByMaterialId = "select UnitId,unit,sum(OnHand) OnHand " +
                                                                        "from " +
                                                                        "( " +
                                                                        "	select dtl.UnitId,unit.Code [unit],dtl.OnHand " +
                                                                        "	from ReceivedMaterialDetails dtl " +
                                                                        "	inner join Units unit " +
                                                                        "	on dtl.UnitId=unit.Id " +
                                                                        "	where dtl.MaterialId=@matid " +
                                                                        "	UNION ALL " +
                                                                        "	select mat.DefaultUnitId UnitId,unt.Code unit, dtl.OnHand  " +
                                                                        "	from ReturnedChargedMaterialDetails dtl " +
                                                                        "	inner join Materials mat " +
                                                                        "	on dtl.MaterialId=mat.Id " +
                                                                        "	inner join Units unt " +
                                                                        "	on mat.DefaultUnitId=unt.Id " +
                                                                        "	where dtl.OnHand>0 and dtl.MaterialId=@matid " +
                                                                        ") src " +
                                                                        "group by unitid,unit; ";

        public static readonly string sqlGetMCTByRange = "select dtl.MaterialId,mat.Material[StockName], mat.Description[StockDescription],dtl.SerialNo, hdr.PostedDate,hdr.Id[MCTNo], " +
                                                         "unt.Code[Unit], app.MCTOutQty[Quantity], Round(app.RRUnitCost, 2)[UnitCost], round(app.MCTOutQty * app.RRUnitCost, 2)[TotalCost], jwm.Code[WOCode], " +
                                                         "jwm.Description[WO], jwm.DR_Account[WOAccount], dtl.JOWOMONumber[WONumber], hdr.Project, hdr.ProjectAddress " +
                                                         "from ChargedMaterialHeaders hdr " +
                                                         "inner join ChargedMaterialDetails dtl " +
                                                         "on hdr.Id = dtl.ChargedMaterialHeaderId " +
                                                         "inner join Materials mat " +
                                                         "on dtl.MaterialId = mat.Id " +
                                                         "inner join Units unt " +
                                                         "on dtl.UnitId = unt.Id " +
                                                         "inner join ChargedMaterialApply app " +
                                                         "on dtl.Id = app.MCTDetailId " +
                                                         "inner join JOWOMO jwm " +
                                                         "on dtl.JOWOMOId = jwm.Id " +
                                                         "where hdr.PostedDate between @dateFrom and @dateTo " +
                                                         "order by MCTNo; ";
    }
}