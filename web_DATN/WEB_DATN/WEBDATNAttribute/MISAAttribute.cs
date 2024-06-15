using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.MISAAttribute
{
    #region 
    /// <summary>
    /// đánh đấu những trường dữ liệu không được để trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {

    }
    /// <summary>
    /// lấy ra tên của thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyName : Attribute
    {
        public string Name = string.Empty;
        public PropertyName(string name)
        {
            Name = name;

        }

    }
    /// <summary>
    /// đánh dấu trường là khóa chính 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimayKey : Attribute
    {

    }
    /// <summary>
    /// đánh dấu cột để bỏ qua khi thêm mới
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMapInsert : Attribute
    {

    }
    #endregion


}
