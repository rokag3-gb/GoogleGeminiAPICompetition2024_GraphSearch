namespace GraphSearchAPI;

public record class MspModel
{
    public long MspId { get; set; } // MSP 계약 id
    public string CorpName { get; set; } // 회사명
    public string? OrgName { get; set; } // 조직명 (Zendesk, Freshdesk 기준)
    public string? CorpDomain { get; set; } // 도메인
    public string? CorpAddress { get; set; } // 주소
    public string VendorCode { get; set; } // 벤더: Azure, AWS, Datadog 등
    public long ServerId { get; set; } // 계약주체(AccountId): ALCM, GSN
    public string ServiceType { get; set; } // 계약형태: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // 계약일자
    public DateTime? RefreshDate { get; set; } // 갱신일자
    public string? Salesman { get; set; } // 담당Sales
    public string? PIC1Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // 계정정보 (CSV형태)
    public string? Memo { get; set; } // 메모
    public string? UpdateMemo { get; set; } // 변경 사항 메모
    public DateTime SavedAt { get; set; } // 저장일시
    public string? SaverId { get; set; } // 저장자
    public bool IsActive { get; set; } // 활성여부
}

public record class MspDto
{
    public long MspId { get; set; } // MSP 계약 id
    public string CorpName { get; set; } // 회사명
    public string? OrgName { get; set; } // 조직명 (Zendesk, Freshdesk 기준)
    public string? CorpDomain { get; set; } // 도메인
    public string? CorpAddress { get; set; } // 주소
    public string VendorCode { get; set; } // 벤더: Azure, AWS, Datadog 등
    public string VendorName { get; set; } // 벤더네임
    public long ServerId { get; set; } // 계약주체(AccountId): ALCM, GSN
    public string ServerName { get; set; } // 계약주체명(AccountName)
    public string ServiceType { get; set; } // 계약형태: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // 계약일자
    public DateTime? RefreshDate { get; set; } // 갱신일자
    public string? Salesman { get; set; } // 담당Sales
    public string? PIC1Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // 계정정보 (CSV형태)
    public string? Memo { get; set; } // 메모
    public string? UpdateMemo { get; set; } // 변경 사항 메모
    public DateTime SavedAt { get; set; } // 저장일시
    public string? SaverId { get; set; } // 저장자
    public string? SaverName { get; set; } // 저장자명
    public bool IsActive { get; set; } // 활성여부
}

public partial class MspUpdateDto
{
    public string CorpName { get; set; } // 회사명
    public string? OrgName { get; set; } // 조직명 (Zendesk, Freshdesk 기준)
    public string? CorpDomain { get; set; } // 도메인
    public string? CorpAddress { get; set; } // 주소
    public string VendorCode { get; set; } // 벤더: Azure, AWS, Datadog 등
    public long ServerId { get; set; } // 계약주체(AccountId): ALCM, GSN
    public string ServiceType { get; set; } // 계약형태: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // 계약일자
    public DateTime? RefreshDate { get; set; } // 갱신일자
    public string? Salesman { get; set; } // 담당Sales
    public string? PIC1Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge 담당자(정) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // 계정정보 (CSV형태)
    public string? Memo { get; set; } // 메모
    public string? UpdateMemo { get; set; } // 변경 사항 메모
    //public DateTime SavedAt { get; set; }
    public string? SaverId { get; set; } // 저장자
}