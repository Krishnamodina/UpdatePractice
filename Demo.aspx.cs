using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UpdatePractice
{
    public partial class Demo : System.Web.UI.Page
    {
        String k = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridview();
            }
        }
        private void BindGridview()
        {
            SqlConnection con = new SqlConnection(k);
            SqlDataAdapter da = new SqlDataAdapter("select * from MT_Persons", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridview();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            Label l1 = (Label)row.FindControl("Label2");
            SqlConnection con = new SqlConnection(k); 
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete from MT_Persons where PersonID='" + l1.Text + "'",con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox id = (TextBox)row.FindControl("TextBox1");
            TextBox Pname = (TextBox)row.FindControl("TextBox2");
            TextBox Age = (TextBox)row.FindControl("TextBox3");
            TextBox Sex = (TextBox)row.FindControl("TextBox4");
            TextBox City = (TextBox)row.FindControl("TextBox5");
            TextBox Hobby1 = (TextBox)row.FindControl("TextBox6");
            TextBox Hobby2 = (TextBox)row.FindControl("TextBox7");
            TextBox Hobby3 = (TextBox)row.FindControl("TextBox8");
            SqlConnection con = new SqlConnection(k);
            con.Open();
            string query = "UPDATE MT_Persons set PersonName='" + Pname.Text + "',Age='" + Age.Text + "',Sex='" + Sex.Text + "',City='" + City + "',Hobby1='" + Hobby1.Text + "',Hobby2='" + Hobby2.Text + "',Hobby3='" + Hobby3.Text + "'";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.ExecuteNonQuery();
            con.Close();
            BindGridview();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridview();
        }

       
    }
}