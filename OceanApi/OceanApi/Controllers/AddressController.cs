using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OceanApi.EntityFramework;
using OceanApi.EntityFramework.Entities;

namespace OceanApi.Controllers
{
    /// <summary>
    /// 地址类接口
    /// </summary>
    [Route("/api/address")]
    public class AddressController : Controller
    {
        private readonly OceanApiContext context;

        /// <summary>
        /// DbContext注入的构造函数
        /// </summary>
        /// <param name="_context"></param>
        public AddressController(OceanApiContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// 获取前十条Address记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAll")]
        public IList<Address> GetAll()
        {
            return context.Address.Take(10).ToList();
        }
    }
}