using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.HCT.ETax.ApplicationCore.Entities.Common
{
    public class ObjectEquals
    {
        /// <summary>
        /// Hàm so sánh xem 2 object có thông tin khác nhau hay không
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="tsource">Object nguồn</param>
        /// <param name="ttarget">Object cần so sánh</param>
        /// <param name="listProperty">Danh sách property cần so sánh</param>
        /// created nvbinh2 10.11.2020
        /// <returns></returns>
        public static bool CheckEqualsObject<X,Y>(X tsource, Y ttarget, List<string> listProperty)
        {
            foreach(var i in listProperty)
            {
                var propertySource = tsource.GetType().GetProperty(i).GetValue(tsource);
                var propertyTarget = ttarget.GetType().GetProperty(i).GetValue(ttarget);
                if(propertyTarget == null && propertyTarget == null)
                {
                    continue;
                }
                if(propertyTarget == null || propertySource == null)
                {
                    return false;
                }
                if(!propertySource.Equals(propertyTarget))
                {
                    return false;
                }

            }
            return true;
        }
    }
}
