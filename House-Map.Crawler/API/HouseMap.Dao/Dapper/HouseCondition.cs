using Dapper;
using HouseMap.Dao.DBEntity;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HouseMap.Dao
{
    public class HouseCondition
    {
        public string CityName { get; set; }
        public string Source { get; set; } = "";
        public int HouseCount { get; set; } = 600;
        public int IntervalDay { get; set; } = 14;
        public string Keyword { get; set; } = "";
        public bool Refresh { get; set; }
        public int Page { get; set; } = 0;
        public int FromPrice { get; set; } = 0;
        public int ToPrice { get; set; } = 0;

        public long HouseId { get; set; } = 0;

        public string RedisKey
        {
            get
            {
                if (this.HouseId != 0)
                {
                    return $"{this.CityName}-{this.Source}-{this.HouseId}";
                }
                var key = $"{this.CityName}-{this.Source}-{this.IntervalDay}-{this.HouseCount}-{this.Keyword}-{this.Page}";
                if (this.FromPrice > 0 && this.ToPrice > 0 && this.FromPrice <= this.ToPrice)
                {
                    key = key + $"{-this.FromPrice}-{this.ToPrice}";
                }
                return key;
            }
        }

        public DateTime PubTime
        {
            get
            {
                return DateTime.Now.Date.AddDays(-IntervalDay);
            }
        }

        public string LikeKeyWord
        {
            get { return $"%{Keyword}%"; }
        }

        public string QueryText
        {
            get
            {

                string queryText = @"SELECT Id,
                                            HouseOnlineURL,
                                            HouseTitle,
                                            HouseLocation,
                                            DisPlayPrice,
                                            PubTime,
                                            HousePrice,
                                            LocationCityName,
                                            Source,
                                            PicURLs"
                                   + $" from { ConstConfigName.GetTableName(this.Source)} where 1=1 ";
                if (this.HouseId != 0)
                {
                    queryText = queryText + $" and Id = @HouseId ";
                    return queryText;
                }
                queryText = queryText + $" and LocationCityName = @CityName and  PubTime >= @PubTime ";
                if (!string.IsNullOrEmpty(this.Keyword))
                {
                    queryText = queryText + " and (HouseText like @LikeKeyWord or HouseLocation like @LikeKeyWord) ";
                }
                if (this.FromPrice > 0 && this.ToPrice >= 0 && this.FromPrice <= this.ToPrice)
                {
                    queryText = queryText + $" and (HousePrice >= {this.FromPrice} and HousePrice <={this.ToPrice}) "
                    + $" order by HousePrice, PubTime limit {this.HouseCount * this.Page}, {this.HouseCount}";
                }
                else
                {
                    queryText = queryText + $" order by PubTime desc limit {this.HouseCount * this.Page}, {this.HouseCount} ";
                }
                return queryText;

            }
        }
    }




}
