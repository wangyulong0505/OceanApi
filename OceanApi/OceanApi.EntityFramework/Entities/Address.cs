using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OceanApi.EntityFramework.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        /// <summary>
        /// 
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否是默认地址
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
