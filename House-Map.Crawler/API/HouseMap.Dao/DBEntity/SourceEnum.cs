using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HouseMap.Common;
using Newtonsoft.Json;

namespace HouseMap.Dao.DBEntity
{
    public enum SourceEnum
    {
        [Source("douban", "DoubanHouse", "豆瓣小组")]
        Douban,

        [Source("douban_wechat", "DoubanWechatHouse", "豆瓣租房")]
        DoubanWechat,

        [Source("pinpaigongyu", "ApartmentHouse", "公寓")]
        PinPaiGongYu,

        [Source("huzhuzufang", "HuzuHouse", "互助租房")]
        HuZhuZuFang,

        [Source("ccbhouse", "CCBHouse", "CCB建融")]
        CCBHouse,

        [Source("zuber", "ZuberHouse", "Zuber")]
        Zuber,

        [Source("hkspacious", "HKHouse", "千居")]
        HKSpacious,

        [Source("mogu", "MoguHouse", "蘑菇租房")]
        Mogu,
        [Source("baixing", "BaixingHouse", "百姓网")]
        BaiXing,

        [Source("baixing_wechat", "BaixingHouse", "百姓网")]
        BaixingWechat,

        [Source("beike", "BeikeHouse", "贝壳")]
        Beike,
        [Source("chengdufgj", "ChengduHouse", "成都房建局")]
        Chengdufgj


    }


    public static class SourceTool
    {
        public static Dictionary<string, string> GetHouseTableNameDic()
        {
            var dic = new Dictionary<string, string>();
            foreach (SourceEnum sourceEnum in Enum.GetValues(typeof(SourceEnum)))
            {
                dic.Add(sourceEnum.GetSourceName(), sourceEnum.GetTableName());
            }
            return dic;
        }

         public static Dictionary<string, string> GetDescriptionDic()
        {
            var dic = new Dictionary<string, string>();
            foreach (SourceEnum sourceEnum in Enum.GetValues(typeof(SourceEnum)))
            {
                dic.Add(sourceEnum.GetSourceName(), sourceEnum.GetEnumDescription());
            }
            return dic;
        }
    }

}