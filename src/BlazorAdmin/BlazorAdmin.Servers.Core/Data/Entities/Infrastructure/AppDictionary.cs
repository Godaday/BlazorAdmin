using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAdmin.Servers.Core.Data.Entities.Infrastructure;

/// <summary>
/// 业务数据字典映射类
/// </summary>
[Table("APP_DICTIONNARY")] 
[Comment("业务数据字典")] 
public class AppDictionary
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Key] // 标识为主键
    [Comment("主键")] 
    public int Id { get; set; }



    /// <summary>
    /// 字典项的编码
    /// </summary>
    [Required] // 不能为空
    [StringLength(100)] // 限制长度
    [Comment("编码")] 
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 字典项的名称或显示值
    /// </summary>
    [Required] // 不能为空
    [StringLength(200)] // 限制长度
    [Comment("名称")] 
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 字典项的额外值 (可选)
    /// </summary>
    [Comment("额外值")] 
    public string? Value { get; set; } 

    /// <summary>
    /// 排序顺序
    /// </summary>
    [Comment("排序")] 
    public int SortOrder { get; set; }

    /// <summary>
    /// 字典项的详细描述
    /// </summary>
    [Comment("描述")] 
    public string? Description { get; set; } 

    /// <summary>
    /// 是否启用此字典项
    /// </summary>
    [Comment("是否启用")] 
    public bool IsActive { get; set; }

    /// <summary>
    /// 所属父级
    /// </summary>
    [Comment("分类编码")]
    public string? ParentCode { get; set; }
}
