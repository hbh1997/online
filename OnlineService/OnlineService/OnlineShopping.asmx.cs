using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace OnlineService
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

        [WebMethod]
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
        [WebMethod]
        //用户下订单，传入订单列表和用户名
        //返回成功与否
        //订单列表格式如下“商品id/数量，商品id/数量，……”
        public Boolean MakeOrder(String orderList, String user_id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable t1 = new DataTable();
            t1 = conn.ExecuteQuery("select * from order");
            int order_id = t1.Rows.Count+1;
            String sql = "insert into order values('','等待揽件',''," + order_id + "," + orderList + "," + user_id + ")";
            int ret = conn.ExecuteUpdate(sql);

            if (ret > 0)
                return true;
            else
                return false;
        }
        [WebMethod]
        //获取所有的订单数据
        //返回一个order结构体数组
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
                o[i].producer_id= int.Parse(orderinfo.Rows[i]["producer_id"].ToString());
            }

            return o;
        }
        [WebMethod]
        //获取特定的订单数据，传入用户名
        //返回一个order结构体
        public order getOrder(String user_name)
        {
            MySQLConn conn = new MySQLConn();
            DataTable user = new DataTable();
            DataTable orderinfo = new DataTable();
            user = conn.ExecuteQuery("select * from user where user_name='"+user_name+"'");
            String user_id = user.Rows[0]["user_id"].ToString();
            orderinfo = conn.ExecuteQuery("select * from order where user_id=" +user_id);
            order o;

            o.id = int.Parse(orderinfo.Rows[0]["order_id"].ToString());
            o.order_list = orderinfo.Rows[0]["order_list"].ToString();
            o.user_id = int.Parse(orderinfo.Rows[0]["user_id"].ToString());
            o.deliver_name = orderinfo.Rows[0]["deliver_name"].ToString();
            o.deliver_state = orderinfo.Rows[0]["deliver_state"].ToString();
            o.deliver_tel = orderinfo.Rows[0]["deliver_tel"].ToString();
            o.producer_id = int.Parse(orderinfo.Rows[0]["producer_id"].ToString());
            return o;
        }
        [WebMethod]
        //获取特定的订单数据，传入生产商id
        //返回一个order结构体
        public order getProducerOrder(String producer_id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable orderinfo = new DataTable();
            orderinfo = conn.ExecuteQuery("select * from order where producer_id=" + producer_id);
            order o;

            o.id = int.Parse(orderinfo.Rows[0]["order_id"].ToString());
            o.order_list = orderinfo.Rows[0]["order_list"].ToString();
            o.user_id = int.Parse(orderinfo.Rows[0]["user_id"].ToString());
            o.deliver_name = orderinfo.Rows[0]["deliver_name"].ToString();
            o.deliver_state = orderinfo.Rows[0]["deliver_state"].ToString();
            o.deliver_tel = orderinfo.Rows[0]["deliver_tel"].ToString();
            o.producer_id = int.Parse(orderinfo.Rows[0]["producer_id"].ToString());
            return o;
        }
        [WebMethod]
        //获取所有的送货员信息
        //返回一个deliverinfo结构体数组
        public deliverinfo[] getAlldeliver()
        {
            MySQLConn conn = new MySQLConn();
            DataTable deliverinfo = new DataTable();
            deliverinfo = conn.ExecuteQuery("select * from deliver");
            int n = deliverinfo.Rows.Count;

            deliverinfo[] o = new deliverinfo[n];

            for(int i = 0; i < n; i++)
            {
                o[i].id = int.Parse(deliverinfo.Rows[i]["deliver_id"].ToString());
                o[i].name = deliverinfo.Rows[i]["deliver_name"].ToString();
                o[i].tel = deliverinfo.Rows[i]["deliver_tel"].ToString();
            }

            return o;
        }
        [WebMethod]
        //物流公司揽件函数，传入送货员id和订单id
        //返回成功与否
        public Boolean updateDeliverState(String deliver_name,int order_id)
        {
            MySQLConn conn = new MySQLConn();

            int result = conn.ExecuteUpdate("update order set deliver_state='已发货' and deliver_name='"+deliver_name+"' where order_id=" + order_id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [WebMethod]
        //用户取消订单函数，传入订单id
        //如果已经取消或者已经被确认收到，则无法取消订单
        //返回成功与否
        public Boolean cancelOrder(int order_id)
        {
            MySQLConn conn = new MySQLConn();
            DataTable info = new DataTable();
            info = conn.ExecuteQuery("select * from order where order_id=" + order_id);
            String deliverstate = info.Rows[0]["deliver_state"].ToString();

            if(!(deliverstate.Equals("已取消")|| deliverstate.Equals("已签收"))){
                int result = conn.ExecuteUpdate("update order set deliver_state='已取消' where order_id=" + order_id);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }else{
                return false;
            }
        }
        [WebMethod]
        //用户确认收货函数。传入订单id
        //返回成功与否
        public Boolean signOrder(int order_id)
        {
            MySQLConn conn = new MySQLConn();

            int result = conn.ExecuteUpdate("update order set deliver_state='已签收' where order_id=" + order_id);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [WebMethod]
        //登陆函数，传入用户名密码
        //登陆成功返回用户角色，失败返回一个错误信息
        public int Login(String user_name,String pwd)
        {
            MySQLConn conn = new MySQLConn();
            DataTable login = new DataTable();

            login = conn.ExecuteQuery("select * from user where user_name='" + user_name + "' and user_pwd='" + pwd + "'");

            if (login.Rows.Count == 0)
            {
                return -1;
            }else
            {
                int role = int.Parse(login.Rows[0]["user_role"].ToString());
                return role;
            }
           
        }
    }
    public struct deliverinfo
    {
        public int id;
        public String name;
        public String tel;
    }
    public struct order
    {
        public int id;
        public String order_list;
        public int user_id;
        public String deliver_name;
        public String deliver_state;
        public String deliver_tel;
        public int producer_id;
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
