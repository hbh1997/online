using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Online
{
    /// <summary>
    /// OnlineShopping 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]

    public class OnlineShopping : System.Web.Services.WebService
    {

        [WebMethod]

        //获取所有的商品数据
        //返回一个product结构体的数组

        public product[] getAllProduct()
        {
            MySQLConn conn = new MySQLConn();
            DataTable productinfo = new DataTable();

            int n;
            productinfo = conn.ExecuteQuery("Select * from product");
            n = productinfo.Rows.Count;
            product[] p = new product[n];

            for (int i = 0; i < n; i++)
            {
                p[i].id = int.Parse(productinfo.Rows[i]["product_id"].ToString());
                p[i].name = productinfo.Rows[i]["product_name"].ToString();
                p[i].price = int.Parse(productinfo.Rows[i]["product_price"].ToString());
                p[i].info = productinfo.Rows[i]["product_info"].ToString();
                p[i].num = int.Parse(productinfo.Rows[i]["product_num"].ToString());
                p[i].producer_id = int.Parse(productinfo.Rows[i]["producer_id"].ToString());
                p[i].producer_name = productinfo.Rows[i]["producer_name"].ToString();
                p[i].image = productinfo.Rows[i]["product_image"].ToString();
            }
            return p;
        }

        //返回一个特定的商品
        //传入商品的id，传回一个product结构体
        public product getProduct(int id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable productinfo = new DataTable();
            productinfo = conn.ExecuteQuery("Select * from product where product_id=" + id);
            product p;
            p.id = int.Parse(productinfo.Rows[0]["product_id"].ToString());
            p.name = productinfo.Rows[0]["product_name"].ToString();
            p.price = int.Parse(productinfo.Rows[0]["product_price"].ToString());
            p.info = productinfo.Rows[0]["product_info"].ToString();
            p.num = int.Parse(productinfo.Rows[0]["product_num"].ToString());
            p.producer_id = int.Parse(productinfo.Rows[0]["producer_id"].ToString());
            p.producer_name = productinfo.Rows[0]["producer_name"].ToString();
            p.image = productinfo.Rows[0]["product_image"].ToString();
            return p;
        }

        public Boolean MakeOrder(String orderList, String user_id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable t1 = new DataTable();
            t1 = conn.ExecuteQuery("select * from order");
            int order_id = t1.Rows.Count;
            String sql = "insert into order values('','等待揽件',''," + order_id + "," + orderList + "," + user_id + ")";
            int ret = conn.ExecuteUpdate(sql);

            if (ret > 0)
                return true;
            else
                return false;
        }

        public order[] getAllOrder()
        {
            MySQLConn conn = new MySQLConn();
            DataTable orderinfo = new DataTable();
            orderinfo = conn.ExecuteQuery("select * from order");
            int n = orderinfo.Rows.Count;

            order[] o = new order[n];

            for (int i = 0; i < n; i++)
            {
                o[i].id = int.Parse(orderinfo.Rows[i]["order_id"].ToString());
                o[i].order_list = orderinfo.Rows[i]["order_list"].ToString();
                o[i].user_id = int.Parse(orderinfo.Rows[i]["user_id"].ToString());
                o[i].deliver_name = orderinfo.Rows[i]["deliver_name"].ToString();
                o[i].deliver_state = orderinfo.Rows[i]["deliver_state"].ToString();
                o[i].deliver_tel = orderinfo.Rows[i]["deliver_tel"].ToString();
            }

            return o;
        }
        public order getOrder(int order_id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable orderinfo = new DataTable();
            orderinfo = conn.ExecuteQuery("select * from order where order_id=" + order_id);
            int n = orderinfo.Rows.Count;

            order o;

            o.id = int.Parse(orderinfo.Rows[0]["order_id"].ToString());
            o.order_list = orderinfo.Rows[0]["order_list"].ToString();
            o.user_id = int.Parse(orderinfo.Rows[0]["user_id"].ToString());
            o.deliver_name = orderinfo.Rows[0]["deliver_name"].ToString();
            o.deliver_state = orderinfo.Rows[0]["deliver_state"].ToString();
            o.deliver_tel = orderinfo.Rows[0]["deliver_tel"].ToString();
            return o;
        }
        public Boolean updateDeliverState()
        {
            return true;
        }
    }
    public struct deliverinfo
    {

    }
    public struct order
    {
        public int id;
        public String order_list;
        public int user_id;
        public String deliver_name;
        public String deliver_state;
        public String deliver_tel;
    }
    public struct product
    {
        public int id;
        public String name;
        public int price;
        public String info;
        public int num;
        public int producer_id;
        public String producer_name;
        public String image;
    }
}
