using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace Laboratorio_09 //DICE LAB 9 PORQUE NO COMPILABA EN EL QUE TENIA EL NOMBRE DEL CASO LAB
   // CasoLaboratorio_03_POOI_T3TL_CANCHARISAMAMEJENNIFER
{
    public partial class Form2 : Form
    {
        string cadena = "Data Source=LAPTOP-TV7UL017\\SQLSERVER;Initial Catalog = Laboratorio1; Integrated Security = true";
        public Form2()
        {
            InitializeComponent();
       
            cboProveedor.DataSource = GetProveedor();
            cboProveedor.DisplayMember = "razonSocial";
            cboProveedor.ValueMember = "idProveedor";

            dgCompras.DataSource = GetProveedores();
        }
        private DataTable GetProveedores()
        {
            SqlConnection connection = new SqlConnection(cadena);
            SqlDataAdapter da = new SqlDataAdapter("sp_GetProveedores", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }


        private DataTable GetProveedor()
        {
            SqlConnection connection = new SqlConnection(cadena);
            SqlDataAdapter da = new SqlDataAdapter("sp_GetProveedor", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(cadena);
            string sentenciaSQL = "sp_GetComprasByFechasProveedor @prmdatFechaInicio, @prmdatFechaFin, @prmintIdProveedor";
            SqlDataAdapter da = new SqlDataAdapter(sentenciaSQL, connection);
            da.SelectCommand.Parameters.AddWithValue("@prmdatFechaInicio", dtInicio.Value);
            da.SelectCommand.Parameters.AddWithValue("@prmdatFechaFin", dtFin.Value);
            da.SelectCommand.Parameters.AddWithValue("@prmintIdProveedor", cboProveedor.SelectedValue);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dgCompras.DataSource = dt;
        }
    }
}
