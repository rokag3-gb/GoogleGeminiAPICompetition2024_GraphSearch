namespace GraphSearchAPI;

public record class MspModel
{
    public long MspId { get; set; } // MSP ��� id
    public string CorpName { get; set; } // ȸ���
    public string? OrgName { get; set; } // ������ (Zendesk, Freshdesk ����)
    public string? CorpDomain { get; set; } // ������
    public string? CorpAddress { get; set; } // �ּ�
    public string VendorCode { get; set; } // ����: Azure, AWS, Datadog ��
    public long ServerId { get; set; } // �����ü(AccountId): ALCM, GSN
    public string ServiceType { get; set; } // �������: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // �������
    public DateTime? RefreshDate { get; set; } // ��������
    public string? Salesman { get; set; } // ���Sales
    public string? PIC1Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // �������� (CSV����)
    public string? Memo { get; set; } // �޸�
    public string? UpdateMemo { get; set; } // ���� ���� �޸�
    public DateTime SavedAt { get; set; } // �����Ͻ�
    public string? SaverId { get; set; } // ������
    public bool IsActive { get; set; } // Ȱ������
}

public record class MspDto
{
    public long MspId { get; set; } // MSP ��� id
    public string CorpName { get; set; } // ȸ���
    public string? OrgName { get; set; } // ������ (Zendesk, Freshdesk ����)
    public string? CorpDomain { get; set; } // ������
    public string? CorpAddress { get; set; } // �ּ�
    public string VendorCode { get; set; } // ����: Azure, AWS, Datadog ��
    public string VendorName { get; set; } // ��������
    public long ServerId { get; set; } // �����ü(AccountId): ALCM, GSN
    public string ServerName { get; set; } // �����ü��(AccountName)
    public string ServiceType { get; set; } // �������: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // �������
    public DateTime? RefreshDate { get; set; } // ��������
    public string? Salesman { get; set; } // ���Sales
    public string? PIC1Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // �������� (CSV����)
    public string? Memo { get; set; } // �޸�
    public string? UpdateMemo { get; set; } // ���� ���� �޸�
    public DateTime SavedAt { get; set; } // �����Ͻ�
    public string? SaverId { get; set; } // ������
    public string? SaverName { get; set; } // �����ڸ�
    public bool IsActive { get; set; } // Ȱ������
}

public partial class MspUpdateDto
{
    public string CorpName { get; set; } // ȸ���
    public string? OrgName { get; set; } // ������ (Zendesk, Freshdesk ����)
    public string? CorpDomain { get; set; } // ������
    public string? CorpAddress { get; set; } // �ּ�
    public string VendorCode { get; set; } // ����: Azure, AWS, Datadog ��
    public long ServerId { get; set; } // �����ü(AccountId): ALCM, GSN
    public string ServiceType { get; set; } // �������: Managed Service Essential, GSN Managed Service, GSN Billing
    public DateTime? ContractDate { get; set; } // �������
    public DateTime? RefreshDate { get; set; } // ��������
    public string? Salesman { get; set; } // ���Sales
    public string? PIC1Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? PIC2Csv { get; set; } // PersonInCharge �����(��) = IAM.dbo.USER_ENTITY.ID
    public string? VendorAccountCsv { get; set; } // �������� (CSV����)
    public string? Memo { get; set; } // �޸�
    public string? UpdateMemo { get; set; } // ���� ���� �޸�
    //public DateTime SavedAt { get; set; }
    public string? SaverId { get; set; } // ������
}