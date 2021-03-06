﻿using System;
using System.Collections.Generic;
using System.Text;
using TravelMeaning.Models.DTO;

namespace TravelMeaning.Models.ResponseModels.User
{
    public class LogInModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 用户号(非ID)
        /// </summary>
        public int UId { get; set; }
        /// <summary>
        /// 用户角色字符串（多种）
        /// </summary>
        public string RolesStr { get; set; }
    }
}
