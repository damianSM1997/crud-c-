using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                double precio_publico = double.Parse(txtPrecioPublico.Text);
                int existencias = int.Parse(txtExistencias.Text);

                if (codigo != "" && nombre != "" && descripcion != "" && precio_publico > 0 && existencias > 0)
                {



                    string sql = "INSERT INTO productos (codigo, nombre , descripcion, precio_publico, existencias) VALUES ('" + codigo + "', '" + nombre + "', '" + descripcion + "', '" + precio_publico + "','" + existencias + "')";

                    MySqlConnection conexionBD = Conexion.conexion();
                    conexionBD.Open();

                    try
                    {
                        //comando de la insercion y la coneccion de nuestra DB
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        // ya se esta ejecutando nuestro comando y la insercion
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro guardado");
                        limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guardar:" + ex.Message);
                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                } else
                {
                    MessageBox.Show("Debes de completar todos los campos");
                }
            } catch(FormatException fex)
            {
                MessageBox.Show("Datos incorrectos " + fex.Message);
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            //aqui se guardaran los datos de la consulta
            MySqlDataReader reader = null;

            string sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias FROM productos WHERE codigo LIKE '" + codigo + "' LIMIT 1";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                //aqui se hace el enlace entre el comando dado por la variable sql y la coneccion 
                // que hace referencia a nuestra clase Conexion en su metoco Conexion
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();

                //si existen filas
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //aqui se pueden poner dos posibles soluciones
                        // el indice de la columna
                        txtId.Text = reader.GetString(0);
                        txtCodigo.Text = reader.GetString(1);
                        txtNombre.Text = reader.GetString(2);
                        txtDescripcion.Text = reader.GetString(3);
                        txtPrecioPublico.Text = reader.GetString(4);
                        txtExistencias.Text = reader.GetString(5);
                    }
                } else
                {
                    MessageBox.Show("No se encontraron registros");
                } 

            } catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar: "+ex.Message);
            } finally
            {
                conexionBD.Close();
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precio_publico = double.Parse(txtPrecioPublico.Text);
            int existencias = int.Parse(txtExistencias.Text);

            string sql = "UPDATE productos SET codigo ='"+codigo+"', nombre='"+nombre+ "' , descripcion='"+descripcion+ "', precio_publico='"+precio_publico+ "', existencias='"+existencias+"' WHERE id='"+id+"'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                //comando de la insercion y la coneccion de nuestra DB
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                // ya se esta ejecutando nuestro comando y la insercion
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Modificado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar:" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            

            string sql = "DELETE FROM productos WHERE id='" + id + "'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();

            try
            {
                //comando de la insercion y la coneccion de nuestra DB
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                // ya se esta ejecutando nuestro comando y la insercion
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar producto:" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        private void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioPublico.Text = "";
            txtExistencias.Text = "";
            txtCodigo.Text = "";
        }
    }


}
