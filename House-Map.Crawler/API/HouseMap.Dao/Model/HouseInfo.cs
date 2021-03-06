﻿//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HouseMap.Common;
using HouseMap.Dao.DBEntity;
using Newtonsoft.Json;

namespace HouseMap.Models
{
    [Serializable()]

    /// <summary>
    /// 
    /// </summary>
    public class HouseInfo
    {
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [Display(Name = "房源标题,建议简单描述房源信息,如:XX小区-两房一厅-主卧3000/月")]
        public string HouseTitle { get; set; }

        [JsonIgnore]
        public string HouseText { get; set; }

        /// <summary>
        /// 房间URL
        /// </summary>
        [Key]
        public string HouseOnlineURL { get; set; }

        /// <summary>
        /// 地理位置（一般用于定位）
        /// </summary>
        public string HouseLocation { get; set; }

        /// <summary>
        /// 价钱（可能非纯数字）
        /// </summary>
        public string DisPlayPrice { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PubTime { get; set; }

        /// <summary>
        /// 价格（纯数字）
        /// </summary>
        public decimal HousePrice { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>

        public string LocationCityName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DataCreateTime { get; set; }

        /// <summary>
        /// 来源网站
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 是否已分析
        /// </summary>
        public bool IsAnalyzed { get; set; }

        /// <summary>
        /// 状态（0:未处理 1：有效 2:已作废）
        /// </summary>
        public int Status { get; set; }

        public string PicURLs { get; set; }

        public List<string> Pictures
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(PicURLs))
                    {
                        return new List<string>();
                    }
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<String>>(PicURLs);
                }
                catch
                {
                    return new List<string>();
                }
            }
        }

        /// <summary>
        /// 定位图标
        /// </summary>
        public string LocationMarkBG
        {
            get
            {
                if (this.HousePrice > 0)
                {
                    return LocationMarkBGType.SelectColor((int)this.HousePrice / 1000);
                }
                return "";
            }
        }
        /// <summary>
        /// 来源名称
        /// </summary>
        public string DisplaySource
        {
            get
            {
                return ConstConfigName.ConvertToDisPlayName(this.Source);
            }
        }

    }
}