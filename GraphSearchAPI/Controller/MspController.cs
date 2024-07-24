namespace GraphSearchAPI;

[ApiController]
//[Route("[controller]")]
public class MspController : ControllerBase
{
    private readonly DapperContext _context;
    //private readonly IAMClient _iamClient;
    //private readonly SalesClient _salesClient;
    private readonly ILogger<MspController> _logger;
    
    public MspController(
        DapperContext context
        //, IAMClient iamClient
        //, SalesClient salesClient
        , ILogger<MspController> logger)
    {
        _context = context;
        //_iamClient = iamClient;
        //_salesClient = salesClient;
        _logger = logger;
    }

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Route("Msps")]
    //public async Task<IEnumerable<MspDto>> GetMsps()
    //{
    //    var conn_str = Secret.dbConnStr;
    //    var token = string.Empty;
    //    token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

    //    try
    //    {
    //        using (IDbConnection conn = _context.CreateConnection(conn_str))
    //        {
    //            if (conn.State != ConnectionState.Open) conn.Open();

    //            var msps = await conn.QueryAsync<MspModel>(
    //                $"select\tMspId, CorpName, OrgName, CorpDomain, CorpAddress, VendorCode, ServerId, ServiceType\r\n" +
    //                $"\t, ContractDate, RefreshDate, Salesman, PIC1Csv, PIC2Csv, VendorAccountCsv, Memo, UpdateMemo, SavedAt, SaverId, IsActive\r\n" +
    //                $"from\tdbo.Msp\r\n" +
    //                $"where\tIsActive = 1\r\n" +
    //                $"order by SavedAt desc" + // 저장일시 내림차순
    //                $";");

    //            if (conn.State == ConnectionState.Open) conn.Close();

    //            // Sale 서비스 통해서 VendorCode에 대한 List<Code> 받아오기
    //            var vendorList = new List<Code>();
    //            //Console.WriteLine($"[Bearer token]={token}");
    //            if (token != string.Empty)
    //                vendorList = await _salesClient.GetKindCodeChilds(token, "VEN");

    //            // msps와 vender 조인
    //            //var joinedMsps = msps.Join(
    //            //    vendorList,
    //            //    i => i.VendorCode?.ToString(),
    //            //    c => c.CodeKey?.ToString(),
    //            //    (i, c) => new { i, c }
    //            //);

    //            // Sale 서비스 통해서 ServerId에 대한 List<Account> 받아오기
    //            var accountList = new List<Account>();
    //            if (token != string.Empty)
    //                accountList = await _salesClient.GetAccounts(token);

    //            // msps와 vendorList, accountList 를 inner join
    //            var joinedMsps2 =
    //                from msp in msps
    //                join vendor in vendorList on msp.VendorCode equals vendor.CodeKey
    //                join account in accountList on msp.ServerId equals account.AccountId
    //                select new {
    //                    msp,
    //                    VendorName = vendor.Name,
    //                    account.AccountName,
    //                };

    //            // IAM 서비스 통해서 SaverIds에 대한 List<UserListItem> 받아오기
    //            var SaverList = new List<UserListItem>();
    //            if (token != string.Empty)
    //                SaverList = await _iamClient.ResolveUserList(token, msps.Select(x => x.SaverId)?.ToHashSet());

    //            // PIC1Csv
    //            //var PIC1CsvList = new List<UserListItem>();
    //            //PIC1CsvList = await _iamClient.ResolveUserList(token, msps.Select(x => x.PIC1Csv).ToHashSet());
    //            //Console.WriteLine(PIC1CsvList);

    //            // PIC2Csv
    //            //var PIC2CsvList = new List<UserListItem>();
    //            //PIC2CsvList = await _iamClient.ResolveUserList(token, msps.Select(x => x.PIC2Csv).ToHashSet());
    //            //Console.WriteLine(PIC2CsvList);

    //            // joinedMsps 에 SaverList 를 left outer join 시킴.
    //            var result =
    //                from i in joinedMsps2
    //                join SaverRaw in SaverList on i.msp.SaverId equals SaverRaw.Id into joinedSavers
    //                from user in joinedSavers.DefaultIfEmpty()
    //                    //join PIC1CsvRaw in PIC1CsvList on i.i.PIC1Csv equals PIC1CsvRaw.Id into joinedPIC1Csv
    //                    //from pic1 in joinedPIC1Csv.DefaultIfEmpty()
    //                select new MspDto()
    //                {
    //                    MspId = i.msp.MspId,
    //                    CorpName = i.msp.CorpName,
    //                    OrgName = i.msp.OrgName,
    //                    CorpDomain = i.msp.CorpDomain,
    //                    CorpAddress = i.msp.CorpAddress,
    //                    VendorCode = i.msp.VendorCode,
    //                    VendorName = i.VendorName ?? "",
    //                    ServerId = i.msp.ServerId,
    //                    ServerName = i.AccountName ?? "",
    //                    ServiceType = i.msp.ServiceType,
    //                    ContractDate = i.msp.ContractDate,
    //                    RefreshDate = i.msp.RefreshDate,
    //                    Salesman = i.msp.Salesman,
    //                    PIC1Csv = i.msp.PIC1Csv,
    //                    //PIC1CsvName = pic1.Username ?? "",
    //                    PIC2Csv = i.msp.PIC2Csv,
    //                    VendorAccountCsv = i.msp.VendorAccountCsv,
    //                    Memo = i.msp.Memo,
    //                    UpdateMemo = i.msp.UpdateMemo,
    //                    SavedAt = i.msp.SavedAt,
    //                    SaverId = i.msp.SaverId,
    //                    SaverName = user?.Username ?? "",
    //                    IsActive = i.msp.IsActive ? true : false,
    //                };

    //            //var result =
    //            //    from i in joinedMsps
    //            //    join SaverRaw in SaverList on i.i.SaverId equals SaverRaw.Id into joinedSavers
    //            //    from user in joinedSavers.DefaultIfEmpty()
    //            //    //join PIC1CsvRaw in PIC1CsvList on i.i.PIC1Csv equals PIC1CsvRaw.Id into joinedPIC1Csv
    //            //    //from pic1 in joinedPIC1Csv.DefaultIfEmpty()
    //            //    select new MspDto()
    //            //    {
    //            //        MspId = i.i.MspId,
    //            //        CorpName = i.i.CorpName,
    //            //        OrgName = i.i.OrgName,
    //            //        CorpDomain = i.i.CorpDomain,
    //            //        CorpAddress = i.i.CorpAddress,
    //            //        VendorCode = i.i.VendorCode,
    //            //        VendorName = i.c?.Name ?? "",
    //            //        ServerId = i.i.ServerId,
    //            //        ServiceType = i.i.ServiceType,
    //            //        ContractDate = i.i.ContractDate,
    //            //        RefreshDate = i.i.RefreshDate,
    //            //        Salesman = i.i.Salesman,
    //            //        PIC1Csv = i.i.PIC1Csv,
    //            //        //PIC1CsvName = pic1.Username ?? "",
    //            //        PIC2Csv = i.i.PIC2Csv,
    //            //        VendorAccountCsv = i.i.VendorAccountCsv,
    //            //        Memo = i.i.Memo,
    //            //        UpdateMemo = i.i.UpdateMemo,
    //            //        SavedAt = i.i.SavedAt,
    //            //        SaverId = i.i.SaverId,
    //            //        SaverName = user?.Username ?? ""
    //            //    };

    //            return result;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Route("Msp/{MspId}")]
    //public async Task<IEnumerable<MspDto>> GetMspById(long MspId)
    //{
    //    var conn_str = Secret.dbConnStr;
    //    var token = string.Empty;
    //    token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

    //    try
    //    {
    //        var param = new DynamicParameters();
    //        param.Add("MspId", MspId);

    //        using (IDbConnection conn = _context.CreateConnection(conn_str))
    //        {
    //            if (conn.State != ConnectionState.Open) conn.Open();

    //            var msps = await conn.QueryAsync<MspModel>(
    //                $"select\tMspId, CorpName, OrgName, CorpDomain, CorpAddress, VendorCode, ServerId, ServiceType\r\n" +
    //                $"\t, ContractDate, RefreshDate, Salesman, PIC1Csv, PIC2Csv, VendorAccountCsv, Memo, UpdateMemo, SavedAt, SaverId, IsActive\r\n" +
    //                $"from\tdbo.Msp\r\n" +
    //                $"where\tMspId = @MspId;", param);

    //            if (conn.State == ConnectionState.Open) conn.Close();

    //            // Sale 서비스 통해서 VendorCode에 대한 List<Code> 받아오기
    //            var vendorList = new List<Code>();
    //            if (token != string.Empty)
    //                vendorList = await _salesClient.GetKindCodeChilds(token, "VEN");

    //            // Sale 서비스 통해서 ServerId에 대한 List<Account> 받아오기
    //            var accountList = new List<Account>();
    //            if (token != string.Empty)
    //                accountList = await _salesClient.GetAccounts(token);

    //            // msps와 vendorList, accountList 를 inner join
    //            var joinedMsps2 =
    //                from msp in msps
    //                join vendor in vendorList on msp.VendorCode equals vendor.CodeKey
    //                join account in accountList on msp.ServerId equals account.AccountId
    //                select new
    //                {
    //                    msp,
    //                    VendorName = vendor.Name,
    //                    account.AccountName,
    //                };

    //            // IAM 서비스 통해서 SaverIds에 대한 List<UserListItem> 받아오기
    //            var SaverList = new List<UserListItem>();
    //            if (token != string.Empty)
    //                SaverList = await _iamClient.ResolveUserList(token, msps.Select(x => x.SaverId)?.ToHashSet());

    //            // joinedMsps 에 SaverList 를 left outer join 시킴.
    //            var result =
    //                from i in joinedMsps2
    //                join SaverRaw in SaverList on i.msp.SaverId equals SaverRaw.Id into joinedSavers
    //                from user in joinedSavers.DefaultIfEmpty()
    //                select new MspDto()
    //                {
    //                    MspId = i.msp.MspId,
    //                    CorpName = i.msp.CorpName,
    //                    OrgName = i.msp.OrgName,
    //                    CorpDomain = i.msp.CorpDomain,
    //                    CorpAddress = i.msp.CorpAddress,
    //                    VendorCode = i.msp.VendorCode,
    //                    VendorName = i.VendorName ?? "",
    //                    ServerId = i.msp.ServerId,
    //                    ServerName = i.AccountName ?? "",
    //                    ServiceType = i.msp.ServiceType,
    //                    ContractDate = i.msp.ContractDate,
    //                    RefreshDate = i.msp.RefreshDate,
    //                    Salesman = i.msp.Salesman,
    //                    PIC1Csv = i.msp.PIC1Csv,
    //                    PIC2Csv = i.msp.PIC2Csv,
    //                    VendorAccountCsv = i.msp.VendorAccountCsv,
    //                    Memo = i.msp.Memo,
    //                    UpdateMemo = i.msp.UpdateMemo,
    //                    SavedAt = i.msp.SavedAt,
    //                    SaverId = i.msp.SaverId,
    //                    SaverName = user?.Username ?? "",
    //                    IsActive = i.msp.IsActive ? true : false,
    //                };

    //            return result;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    [HttpPost]
    [Route("Msp")]
    public async Task<ActionResult> Insert(MspUpdateDto _msp)
    {
        var conn_str = Secret.dbConnStr;

        try
        {
            var param = new DynamicParameters();
            param.Add("CorpName", _msp.CorpName);
            param.Add("OrgName", _msp.OrgName);
            param.Add("CorpDomain", _msp.CorpDomain);
            param.Add("CorpAddress", _msp.CorpAddress);
            param.Add("VendorCode", _msp.VendorCode);
            param.Add("ServerId", _msp.ServerId);
            param.Add("ServiceType", _msp.ServiceType);
            param.Add("ContractDate", _msp.ContractDate);
            param.Add("RefreshDate", _msp.RefreshDate);
            param.Add("Salesman", _msp.Salesman);
            param.Add("PIC1Csv", _msp.PIC1Csv);
            param.Add("PIC2Csv", _msp.PIC2Csv);
            param.Add("VendorAccountCsv", _msp.VendorAccountCsv);
            param.Add("Memo", _msp.Memo);
            param.Add("UpdateMemo", _msp.UpdateMemo);
            param.Add("SaverId", _msp.SaverId);

            int effected_row = 0;

            using (IDbConnection conn = _context.CreateConnection(conn_str))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                var rawSql = $"insert into dbo.Msp (CorpName, OrgName, CorpDomain, CorpAddress, VendorCode, ServerId, ServiceType\r\n" +
                    $"\t, ContractDate, RefreshDate, Salesman, PIC1Csv, PIC2Csv, VendorAccountCsv, Memo, UpdateMemo, SaverId)\r\n" +
                    $"values (@CorpName, @OrgName, @CorpDomain, @CorpAddress, @VendorCode, @ServerId, @ServiceType\r\n" +
                    $"\t, @ContractDate, @RefreshDate, @Salesman, @PIC1Csv, @PIC2Csv, @VendorAccountCsv, @Memo, @UpdateMemo, @SaverId);";

                effected_row = conn.Execute(rawSql, param);

                if (conn.State == ConnectionState.Open) conn.Close();
            }

            var data = new { effected_row = effected_row };
            return StatusCode(200, JsonSerializer.Serialize(data));
            //return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("Msp/{MspId}")]
    public async Task<ActionResult> Update(long MspId, MspUpdateDto _msp)
    {
        var conn_str = Secret.dbConnStr;

        try
        {
            var param = new DynamicParameters();
            param.Add("MspId", MspId);
            param.Add("CorpName", _msp.CorpName);
            param.Add("OrgName", _msp.OrgName);
            param.Add("CorpDomain", _msp.CorpDomain);
            param.Add("CorpAddress", _msp.CorpAddress);
            param.Add("VendorCode", _msp.VendorCode);
            param.Add("ServerId", _msp.ServerId);
            param.Add("ServiceType", _msp.ServiceType);
            param.Add("ContractDate", _msp.ContractDate);
            param.Add("RefreshDate", _msp.RefreshDate);
            param.Add("Salesman", _msp.Salesman);
            param.Add("PIC1Csv", _msp.PIC1Csv);
            param.Add("PIC2Csv", _msp.PIC2Csv);
            param.Add("VendorAccountCsv", _msp.VendorAccountCsv);
            param.Add("Memo", _msp.Memo);
            param.Add("UpdateMemo", _msp.UpdateMemo);
            param.Add("SaverId", _msp.SaverId);

            int effected_row = 0;

            using (IDbConnection conn = _context.CreateConnection(conn_str))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                var rawSql = $"update\tdbo.Msp\r\n"+
                    $"set CorpName = @CorpName\r\n" +
                    $"\t, OrgName = @OrgName\r\n" +
                    $"\t, CorpDomain = @CorpDomain\r\n" +
                    $"\t, CorpAddress = @CorpAddress\r\n" +
                    $"\t, VendorCode = @VendorCode\r\n" +
                    $"\t, ServerId = @ServerId\r\n" +
                    $"\t, ServiceType = @ServiceType\r\n" +
                    $"\t, ContractDate = @ContractDate\r\n" +
                    $"\t, RefreshDate = @RefreshDate\r\n" +
                    $"\t, Salesman = @Salesman\r\n" +
                    $"\t, PIC1Csv = @PIC1Csv\r\n" +
                    $"\t, PIC2Csv = @PIC2Csv\r\n" +
                    $"\t, VendorAccountCsv = @VendorAccountCsv\r\n" +
                    $"\t, Memo = @Memo\r\n" +
                    $"\t, UpdateMemo = @UpdateMemo\r\n" +
                    $"\t, SavedAt = getdate()\r\n" +
                    $"\t, SaverId = @SaverId\r\n" +
                    $"where\tMspId = @MspId;";

                effected_row = conn.Execute(rawSql, param);

                if (conn.State == ConnectionState.Open) conn.Close();
            }

            var data = new { effected_row = effected_row };
            return StatusCode(200, JsonSerializer.Serialize(data));
            //return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("Msp/{MspId}")]
    public async Task<ActionResult> Delete(long MspId)
    {
        var conn_str = Secret.dbConnStr;

        try
        {
            var param = new DynamicParameters();
            param.Add("MspId", MspId);
                
            int effected_row = 0;

            using (IDbConnection conn = _context.CreateConnection(conn_str))
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                //var rawSql = $"delete\r\n" +
                //    $"from\tdbo.Msp\r\n" +
                //    $"where\tMspId = @MspId;";

                var rawSql = $"update m\r\n" +
                    $"set\tm.IsActive = 0\r\n" +
                    $"from\tdbo.Msp m\r\n" +
                    $"where\tm.MspId = @MspId;";

                effected_row = conn.Execute(rawSql, param);
                    
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            var data = new { effected_row = effected_row };
            return StatusCode(200, JsonSerializer.Serialize(data));
            //return NoContent(); // 204
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}