using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AccesoBaseDatos1
{
    public partial class Form1 : Form
    {
        private string Servidor = "ACARDENAS\\SQLDEVELOP2008R2";
        private string Basedatos = "ESCOLAR";
        private string UsuarioId = "sa";
        private string Password = "sa2024";

        private void EjecutaComando(string ConsultaSQL)
        {
            try
            {
                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};" +
                              $"Database={Basedatos};" +
                              $"User Id={UsuarioId};" +
                              $"Password={Password}";

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(ConsultaSQL, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};" +
                              $"Database={Basedatos};" +
                              $"Uid={UsuarioId};" +
                              $"Pwd={Password};";

                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(ConsultaSQL, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }

                llenarGrid();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }
        private void llenarGrid()
        {
            try
            {
                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password}";

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        string sqlQuery = "SELECT * FROM Alumnos";
                        SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, conn);

                        DataSet ds = new DataSet();
                        adp.Fill(ds, "Alumnos");
                        dgvAlumnos.DataSource = ds.Tables[0];
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};Uid={UsuarioId};Pwd={Password};";

                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        string sqlQuery = "SELECT * FROM Alumnos";
                        MySqlDataAdapter adp = new MySqlDataAdapter(sqlQuery, conn);

                        DataSet ds = new DataSet();
                        adp.Fill(ds, "Alumnos");
                        dgvAlumnos.DataSource = ds.Tables[0];
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }

                dgvAlumnos.Refresh();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema: " + Ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrearBD_Click(object sender, EventArgs e)
        {
            try
            {
                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database=master;User Id={UsuarioId};Password={Password}";

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("CREATE DATABASE ESCOLAR", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Base de datos 'ESCOLAR' creada en SQL Server.");
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Uid={UsuarioId};Pwd={Password};";

                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand("CREATE DATABASE ESCOLAR", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Base de datos 'ESCOLAR' creada en MySQL.");
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema: " + Ex.Message);
            }
        }

        private void btnCreaTabla_Click(object sender, EventArgs e)
        {
            EjecutaComando("CREATE TABLE " +
                    "Alumnos (NoControl varchar(10), nombre varchar(50), carrera int)");
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length == 0 ||
                    txtNombre.Text.Trim().Length == 0 ||
                    txtCarrera.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese todos los datos para insertar.");
                    return;
                }

                string consulta = $"INSERT INTO Alumnos (NoControl, nombre, carrera) VALUES " +
                                  $"('{txtNoControl.Text}', '{txtNombre.Text}', '{txtCarrera.Text}')";

                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password}";
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};Uid={UsuarioId};Pwd={Password};";
                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }

                llenarGrid();
                MessageBox.Show("Registro insertado correctamente.");
            }
            catch(SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chkSQLServer.Checked = true;
            llenarGrid();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese el No de Control para eliminar.");
                    return;
                }

                string consulta = $"DELETE FROM Alumnos WHERE NoControl = '{txtNoControl.Text}'";

                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password}";
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};Uid={UsuarioId};Pwd={Password};";
                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }

                llenarGrid();
                MessageBox.Show("Registro eliminado correctamente.");
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length == 0 ||
                    txtNombre.Text.Trim().Length == 0 ||
                    txtCarrera.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese todos los datos para actualizar.");
                    return;
                }

                string consulta = $"UPDATE Alumnos SET nombre = '{txtNombre.Text}', carrera = '{txtCarrera.Text}' " +
                                  $"WHERE NoControl = '{txtNoControl.Text}'";

                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password}";
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};Uid={UsuarioId};Pwd={Password};";
                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand(consulta, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                    return;
                }

                llenarGrid();
                MessageBox.Show("Registro actualizado correctamente.");
            }
            catch (SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Ingrese el No de Control para buscar.");
                    return;
                }

                string strConn;

                if (chkSQLServer.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};User Id={UsuarioId};Password={Password}";

                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT nombre, carrera FROM Alumnos WHERE NoControl = @NoControl", conn))
                        {
                            cmd.Parameters.AddWithValue("@NoControl", txtNoControl.Text);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtNombre.Text = reader["nombre"].ToString();
                                    txtCarrera.Text = reader["carrera"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el registro.");
                                }
                            }
                        }
                    }
                }
                else if (chkMySQL.Checked)
                {
                    strConn = $"Server={Servidor};Database={Basedatos};Uid={UsuarioId};Pwd={Password};";

                    using (MySqlConnection conn = new MySqlConnection(strConn))
                    {
                        conn.Open();
                        using (MySqlCommand cmd = new MySqlCommand("SELECT nombre, carrera FROM Alumnos WHERE NoControl = @NoControl", conn))
                        {
                            cmd.Parameters.AddWithValue("@NoControl", txtNoControl.Text);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtNombre.Text = reader["nombre"].ToString();
                                    txtCarrera.Text = reader["carrera"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el registro.");
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una base de datos.");
                }
            }
            catch(SqlException Ex)
            {
                MessageBox.Show("Error SQL Server: " + Ex.Message);
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show("Error MySQL: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void chkSQLServer_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
