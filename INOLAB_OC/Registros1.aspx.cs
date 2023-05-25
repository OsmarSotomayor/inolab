﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;


namespace INOLAB_OC
{
    public partial class Registros1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string valor = Request.QueryString["valor"];
            //if (Request.Params["valor"] != null)
            //{
            //    lbluser.Text = Request.Params["valor"];
            //    lblidarea.Text = Request.Params["idar"];
            //    lblrol.Text = Request.Params["idr"];
            //}
            //testgit
            if (Session["valor"] == null)
            {
                Response.Redirect("./Sesion.aspx");

            }
            else
            {
                //titulo.Text = "Detalle de FSR N°. " + Session["folio_p"].ToString();
                lbluser.Text = Session["valor"].ToString();
                lblidarea.Text = Session["idar"].ToString();
                lblrol.Text = Session["idr"].ToString();
            }

            if (lblidarea.Text == "1" || lblidarea.Text == "3")
            {
                btnNuevoOC_Servicio.Visible = false;
            }
            else
            {
                Button1.Visible = true; // Botn Salir
            }

            if (lblrol.Text == "2")
            {
                btnOC_Equipo.Visible = false;
            }
            else
            {
                btnOC_Equipo.Visible = true;
            }

            if (lblidarea.Text == "3")
            {
                btnNuevoOC_Equipo.Visible = false;
            }

        }

        SqlConnection con = new SqlConnection(@"Data Source=INOLABSERVER03;Initial Catalog=Browser;Persist Security Info=True;User ID=ventas;Password=V3ntas_17");

        protected void btnvautorizar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                //Response.Write("<script>alert('Se han Autorizado los Registros Seleccionados');</script>");

                CheckBox status = (row.Cells[1].FindControl("CheckBox1") as CheckBox), status_S = (row.Cells[3].FindControl("CheckBox2") as CheckBox), status_c = (row.Cells[2].FindControl("CheckBox3") as CheckBox); ;


                int id = Convert.ToInt32(row.Cells[3].Text);


                if (status.Checked)
                {
                    updaterow(id, "true");

                }
                else
                {
                    updaterow(id, "false");
                }
                if (status_S.Checked)
                {
                    updaterow_s(id, "true");
                }
                else
                {
                    updaterow_s(id, "false");
                }
                if (status_c.Checked)
                {
                    updaterow_c(id, "true");
                }
                else
                {
                    updaterow_c(id, "false");
                }


            }
            Response.Write("<script>alert('Se han Autorizado los Registros Seleccionados');</script>");
            cargardatos();
            //string valor = lbluser.Text, idar = lblidarea.Text, idr = lblrol.Text;
            //Response.Redirect("Registros.aspx?valor=" + valor + "&idar=" + idar + "&idr=" + idr);


            //Response.Write("<script>alert('Se han Autorizado los Registros Seleccionados');</script>");
        }
        private void updaterow(int id, String markstatus)
        {
            String updatedata = "update serviciooc set Admon_V='" + markstatus + "' where idoc_s=" + id;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updatedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            // idr.Text = "Datos actualizados";

            con.Close();

        }
        private void updaterow_s(int ids, String markstatus)
        {
            String updatedata = "update serviciooc set serv_V='" + markstatus + "' where idoc_s=" + ids;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updatedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            //idr.Text = "Datos actualizados";
            con.Close();
        }
        private void updaterow_c(int idc, String markstatus)
        {
            String updatedata = "update servicio_oc set comercial_V='" + markstatus + "' where idoc_s=" + idc;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = updatedata;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            //idr.Text = "Datos actualizados";
            con.Close();
        }
        private void cargardatos()
        {
            SqlCommand cmd = new SqlCommand("Select *from  serviciooc order by idoc_s desc", con);
            //SqlCommand cmd = new SqlCommand("Select *from  servicio_oc order by idoc_s desc", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            // con.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            //con.Close();

            GridView1.DataSource = objdataset;
            GridView1.DataBind();
            lbln_registros.Text = GridView1.Rows.Count.ToString();
        }

        //Boton Registros OC - SERVICIO
        protected void Button2_Click(object sender, EventArgs e)
        {
            //string valor = lbluser.Text;
            //Response.Redirect("Servicio_OC.aspx?valor="+valor);
            string valor = lbluser.Text, idar = lblidarea.Text, idr = lblrol.Text;

            //Response.Redirect("Registros.aspx?valor="+valor);
            //Response.Redirect("Servicio_OC.aspx?valor=" + valor + "&idar=" + idar + "&idr=" + idr);
            Response.Redirect("Servicio_OC1.aspx");
        }

        //Boton Salir
        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Sesion.aspx");
        }

        ////desactivachec guardados en true
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = (CheckBox)e.Row.FindControl("CheckBox1");
                CheckBox cb3 = (CheckBox)e.Row.FindControl("CheckBox3");
                CheckBox cb4 = (CheckBox)e.Row.FindControl("CheckBox2");


                if (lblrol.Text == "1")
                {

                    if (lblidarea.Text == "1")
                    {
                        if (cb.Checked == true)
                        {
                            cb.Enabled = false;
                        }
                        else
                        {
                            cb.Enabled = true;
                        }

                    }

                    if (lblidarea.Text == "2")
                    {
                        if (cb3.Checked == true)
                        {
                            cb3.Enabled = false;
                        }
                        else
                        {
                            cb3.Enabled = true;
                        }

                    }

                    if (lblidarea.Text == "3")
                    {
                        if (cb4.Checked == true)
                        {
                            cb4.Enabled = false;
                        }
                        else
                        {
                            cb4.Enabled = true;
                        }
                    }
                }
                else
                {
                    cb.Enabled = false;
                    cb3.Enabled = false;
                    cb4.Enabled = false;
                }
            }
        }

        string comando = "";
        //Filtro Busqueda
        protected void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string area = "";
            if (lblidarea.Text == "1")
            {
                area = "Admon_V";
            }
            if (lblidarea.Text == "2")
            {
                area = "Serv_V";
            }
            if (cmbFiltro.Text == "Todos")
            {
                //comando = "select *from servicio_oc";
                cargardatos();
                txtbusqueda.Visible = false;
                btnbuscar.Visible = false;
            }
            if (cmbFiltro.Text == "Sin Autorizar")
            {
                comando = "Select *from  serviciooc where " + area + "='false'";
                sentencia();
                txtbusqueda.Visible = false;
                btnbuscar.Visible = false;
            }

            if (cmbFiltro.Text == "Autorizados")
            {
                comando = "select *from serviciooc where " + area + "='true'";
                sentencia();
                txtbusqueda.Visible = false;
                btnbuscar.Visible = false;
            }
            if (cmbFiltro.Text == "OC")
            {
                txtbusqueda.Visible = true;
                btnbuscar.Visible = true;
                txtbusqueda.Text = null;

            }
            if (cmbFiltro.Text == "Cliente")
            {
                txtbusqueda.Visible = true;
                btnbuscar.Visible = true;
                txtbusqueda.Text = null;
            }
            if (cmbFiltro.Text == "Asesor Comercial")
            {
                txtbusqueda.Visible = true;
                btnbuscar.Visible = true;
                txtbusqueda.Text = null;
            }

        }

        public void sentencia()
        {

            SqlCommand cmd = new SqlCommand(comando, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            // con.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            //con.Close();

            GridView1.DataSource = objdataset;
            GridView1.DataBind();
            lbln_registros.Text = GridView1.Rows.Count.ToString();
        }
        //Boton Buscar en Filtro
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            string comando = "";

            if (cmbFiltro.Text == "OC")
            {

                comando = "Select *from  serviciooc where oc like '%" + txtbusqueda.Text + "%'";
            }
            if (cmbFiltro.Text == "Cliente")
            {

                comando = "Select *from  serviciooc where cliente like '%" + txtbusqueda.Text + "%'";
            }
            if (cmbFiltro.Text == "Asesor Comercial")
            {

                comando = "Select *from  serviciooc where Asesor like '%" + txtbusqueda.Text + "%'";
            }
            SqlCommand cmd = new SqlCommand(comando, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand = cmd;
            // con.Open();
            DataSet objdataset = new DataSet();
            adapter.Fill(objdataset);
            //con.Close();

            GridView1.DataSource = objdataset;
            GridView1.DataBind();
            lbln_registros.Text = GridView1.Rows.Count.ToString();
        }

        //Boton Nueva OC Servicio
        protected void Button4_Click(object sender, EventArgs e)
        {
            string valor = lbluser.Text, idar = lblidarea.Text, idr = lblrol.Text;
            //Response.Redirect("Servicio_OC.aspx?valor=" + valor + "&idar=" + idar + "&idr=" + idr);
            Response.Redirect("Servicio_OC1.aspx");
        }

        //Boton Nueva OC Equipo
        protected void Button3_Click(object sender, EventArgs e)
        {
            string valor = lbluser.Text, idar = lblidarea.Text, idr = lblrol.Text;
            //Response.Redirect("Equipo_OC.aspx?valor=" + valor + "&idar=" + idar + "&idr=" + idr);
            Response.Redirect("Equipo_OC1.aspx");
        }

        //Boton OC EQUIPO
        protected void btnOC_Equipo_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Registros_Equipos.aspx?valor=" + lbluser.Text + "&idar=" + lblidarea.Text + "&idr=" + lblrol.Text);
            Response.Redirect("Registro_Equipos1.aspx");
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}