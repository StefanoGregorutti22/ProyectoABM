using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace ABMPersonas
{
    public partial class frmPersona : Form
    {
        SqlConnection conexion = new SqlConnection(@"Data Source=LAPTOP-45JU0EKC;Initial Catalog=TUPPI;Integrated Security=True");
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;
        bool nuevo = false;
        const int tam = 30;
        Persona[] aPersona = new Persona[tam];
        int c;

        public frmPersona()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nuevo = true;
            habilitar(true);
            limpiar();
            txtApellido.Focus();
        }
        private void habilitar(bool x)
        {
            txtApellido.Enabled = x;
            txtNombres.Enabled = x;
            cboTipoDocumento.Enabled = x;
            txtDocumento.Enabled = x;
            cboEstadoCivil.Enabled = x;
            rbtFemenino.Enabled = x;
            rbtMasculino.Enabled = x;
            chkFallecio.Enabled = x;
            btnGrabar.Enabled = x;
        }

        private void limpiar()
        {
            txtApellido.Text = "";
            txtNombres.Text = "";
            cboTipoDocumento.SelectedIndex = -1;
            txtDocumento.Text = "";
            cboEstadoCivil.SelectedIndex = -1;
            rbtFemenino.Checked = false;
            rbtMasculino.Checked = false;
            chkFallecio.Checked = false;
        }

        private DataTable consultarTabla(string nombreTabla)
        {
            DataTable tabla = new DataTable();
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM " + nombreTabla;
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }
        private void cargarCombo(ComboBox combo, string nombreTabla)
        {
            DataTable tabla = consultarTabla(nombreTabla);
            combo.DataSource = tabla;                               //Suponiendo que siempre llenamos un combo con una TABLA AUXILIAR
            combo.ValueMember = tabla.Columns[0].ColumnName;        //Donde el primer campo es la PK
            combo.DisplayMember = tabla.Columns[1].ColumnName;      // y el segundo campo el DESCRIPTOR
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string consultaSql = "";

            if (validarCampos())
            {
                Persona p = new Persona();
                p.pApellido = txtApellido.Text;
                p.pNombres = txtNombres.Text;
                p.pTipoDocumento = Convert.ToInt32(cboTipoDocumento.SelectedValue);
                p.pDocumento = int.Parse(txtDocumento.Text);
                p.pEstadoCivil = Convert.ToInt32(cboEstadoCivil.SelectedValue);
                if (rbtFemenino.Checked)
                    p.pSexo = 1;
                else
                    p.pSexo = 2;
                p.pFallecio = chkFallecio.Checked;

                if (nuevo)
                {
                    consultaSql = "INSERT INTO personas (apellido, nombres, tipo_documento, documento, estado_civil, sexo, fallecio)" +
                                  " VALUES (@apellido,@nombres,@tipo_documento,@documento,@estado_civil,@sexo,@fallecio)";


                    conexion.Open();
                    comando = new SqlCommand();

                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = consultaSql;
                    comando.Parameters.AddWithValue("@apellido", p.pApellido);
                    comando.Parameters.AddWithValue("@nombres", p.pNombres);
                    comando.Parameters.AddWithValue("@tipo_documento", p.pTipoDocumento);
                    comando.Parameters.AddWithValue("@documento", p.pDocumento);
                    comando.Parameters.AddWithValue("@estado_civil", p.pEstadoCivil);
                    comando.Parameters.AddWithValue("@sexo", p.pSexo);
                    comando.Parameters.AddWithValue("@fallecio", p.pFallecio);


                    comando.ExecuteNonQuery();
                    conexion.Close();

                }
                else
                { 
                    // UPDATE usando parámetros
                    consultaSql = "UPDATE personas SET apellido=@apellido," +
                                                       " nombres=@nombres," +
                                                       " tipo_documento=@tipo_documento," +
                                                       " estado_civil=@estado_civil," +
                                                       " sexo=@sexo," +
                                                       " fallecio=@fallecio" +
                                                       " WHERE documento=@documento";
                    conexion.Open();
                    comando = new SqlCommand();
                    comando.Connection = conexion;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = consultaSql;
                    comando.Parameters.AddWithValue("@apellido", p.pApellido);
                    comando.Parameters.AddWithValue("@nombres", p.pNombres);
                    comando.Parameters.AddWithValue("@tipo_documento", p.pTipoDocumento);
                    comando.Parameters.AddWithValue("@estado_civil", p.pEstadoCivil);
                    comando.Parameters.AddWithValue("@sexo", p.pSexo);
                    comando.Parameters.AddWithValue("@fallecio", p.pFallecio);
                    comando.Parameters.AddWithValue("@documento", p.pDocumento);


                    comando.ExecuteNonQuery();
                    conexion.Close();

                }
            

                this.cargarLista(lstPersonas, "personas");

                habilitar(false);
                nuevo = false;
            }
        }

        private bool validarCampos()
        {
            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar un apellido...");
                txtApellido.Focus();
                return false;
            }
            if (txtNombres.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un nombre...");
                txtNombres.Focus();
                return false;
            }
            if (cboTipoDocumento.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo documento...");
                cboTipoDocumento.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                MessageBox.Show("Debe ingresar un numero de documento...");
                txtDocumento.Focus();
                return false;
            }
            else
            {
                try
                {
                    int.Parse(txtDocumento.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Debe ingresar valores numéricos...");
                    txtDocumento.Focus();
                    return false;
                }
            }
            if (cboEstadoCivil.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un estado civil...");
                cboEstadoCivil.Focus();
                return false;
            }
            if (!rbtMasculino.Checked && !rbtFemenino.Checked)
            {
                MessageBox.Show("Debe seleccionar un sexo...");
                rbtFemenino.Focus();
                return false;
            }

            return true;
        }

        private void cargarLista(ListBox lista, string nombreTabla)
        {
            c = 0;
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM " + nombreTabla;
            lector = comando.ExecuteReader();
            while (lector.Read() == true)
                
            {
                //Estos elementos pasan a la parte de abajo para que los nombres aparezcan en la parte derecha del formulario 
                Persona p = new Persona();
                if (!lector.IsDBNull(0))
                    p.pApellido = lector.GetString(0);
                if (!lector.IsDBNull(1))
                    p.pNombres = lector.GetString(1);
                if (!lector.IsDBNull(2))
                    p.pTipoDocumento = (int)lector.GetDecimal(2);
                if (!lector.IsDBNull(3))
                    p.pDocumento = (int)lector.GetDecimal(3);
                if (!lector.IsDBNull(4))
                    p.pEstadoCivil = (int)lector.GetDecimal(4);
                if (!lector.IsDBNull(5))
                    p.pSexo = (int)lector.GetDecimal(5);
                if (!lector.IsDBNull(6))
                    p.pFallecio = lector.GetBoolean(6);

                aPersona[c] = p;
                c++;
            }
            lector.Close();
            conexion.Close();
            //Una vez completado esta parte del codigo carga los nombre en la parte derecha del formulario 
            lista.Items.Clear();
            for (int i = 0; i < c; i++)
            {
                lista.Items.Add(aPersona[i].ToString());

            }
            lista.SelectedIndex = -1;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            habilitar(true);
        

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro de borrar esta persona ?",
                "BORRANDO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // Delete --> borramos el objeto seleccionado en la lista
                string consultaSql = "DELETE FROM personas WHERE documento=" + aPersona[lstPersonas.SelectedIndex].pDocumento;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = consultaSql;
                comando.ExecuteNonQuery();
                conexion.Close();

                this.cargarLista(lstPersonas, "personas");
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            limpiar();
            habilitar(false);
            nuevo = false;
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro de abandonar la aplicación ?",
                "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                this.Close();
        }

        private void cargarCampos(int posicion)
        {
            txtApellido.Text = aPersona[posicion].pApellido;
            txtNombres.Text = aPersona[posicion].pNombres;
            cboTipoDocumento.SelectedValue = aPersona[posicion].pTipoDocumento;
            txtDocumento.Text = aPersona[posicion].pDocumento.ToString();
            cboEstadoCivil.SelectedValue = aPersona[posicion].pEstadoCivil;
            if (aPersona[posicion].pSexo == 1)
                rbtFemenino.Checked = true;
            else
                rbtMasculino.Checked = true;
            chkFallecio.Checked = aPersona[posicion].pFallecio;
        }

        private void frmPersona_Load(object sender, EventArgs e)
        {
            habilitar(false);
            this.cargarLista(lstPersonas, "personas");
            this.cargarCombo(cboEstadoCivil, "estado_civil");
            this.cargarCombo(cboTipoDocumento, "tipo_documento");

        }
    }
}
