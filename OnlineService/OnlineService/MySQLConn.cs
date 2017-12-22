using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Online
{
    class MySQLConn
    {
        private string MySqlConn = "Database=onlineshopping;Data Source=127.0.0.1;User Id = root;password=01089;pooling =true;Charset= utf8;port=3306";

        public DataTable ExecuteQuery (string sqlstr)
        {
            //查询
            MySqlCommand cmd;
            MySqlConnection con;
            MySqlDataAdapter msda;
            con = new MySqlConnection(MySqlConn);
            con.Open();
            cmd = new MySqlCommand(sqlstr,con);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable() ;
            msda = new MySqlDataAdapter(cmd);
            msda.Fill(dt);
            con.Close();
            return dt; 
        }
        public int ExecuteUpdate(string sqlstr)
        {
            MySqlCommand cmd;
            MySqlConnection con;
            con = new MySqlConnection(MySqlConn);
            con.Open();
            cmd = new MySqlCommand(sqlstr,con);
            cmd.CommandType = CommandType.Text;
            int iud = 0;
            iud = cmd.ExecuteNonQuery();
            con.Close();
            return iud; 


        }
    }
}
