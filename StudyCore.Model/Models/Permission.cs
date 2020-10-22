﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static StudyCore.Model.Enums.CommonEnum;

namespace StudyCore.Model
{
    /// <summary>
    /// /// <summary>
    /// 权限实体类
    /// </summary>
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 
        /// </summary>
        public Permission()
        {
            Roles = new HashSet<RolePermissionMapping>();
        }
        /// <summary>
        /// 权限编码
        /// </summary>
        [Required]
        [Key]
        [Column(TypeName = "nvarchar(20)")]
        public string Code { get; set; }
        /// <summary>
        /// 菜单GUID
        /// </summary>
        public Guid MenuGuid { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        /// 权限操作码
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(80)")]
        public string ActionCode { get; set; }
        /// <summary>
        /// 图标(可选)
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 是否已删
        /// </summary>
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 权限类型(0:菜单,1:按钮/操作/功能等)
        /// </summary>
        public PermissionType Type { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid CreatedByUserGuid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByUserName { get; set; }
        /// <summary>
        /// 最近修改时间
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// 最近修改者ID
        /// </summary>
        public Guid? ModifiedByUserGuid { get; set; }
        /// <summary>
        /// 最近修改者姓名
        /// </summary>
        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// 关联的菜单
        /// </summary>
        public Menu Menu { get; set; }
        /// <summary>
        /// 权限所属的角色集合
        /// </summary>
        public ICollection<RolePermissionMapping> Roles { get; set; }
    }
}
