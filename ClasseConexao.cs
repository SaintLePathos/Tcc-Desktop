using System;
using System.Data;
using System.Data.SqlClient;

public class ClasseConexao
{
    private SqlConnection conexao;

    private SqlConnection conectar()
    {
        try
        {
            if (conexao == null)
                conexao = new SqlConnection();

            if (conexao.State != ConnectionState.Open)
            {
                string strConexao = "Password=1234;Persist Security Info=True;User ID=sa;Initial Catalog=Loja_Ecommerce;Data Source= "+Environment.MachineName;//192.168.0.75,1433 ";
                conexao.ConnectionString = strConexao;
                conexao.Open();
            }

            return conexao;
        }
        catch (Exception ex)
        {
            desconectar();
            throw new Exception("Erro ao conectar ao banco de dados: " + ex.Message);
        }
    }

    public void desconectar()
    {
        try
        {
            if (conexao != null && conexao.State == ConnectionState.Open)
            {
                conexao.Close();
                conexao.Dispose();
                conexao = null;
            }
        }
        catch (Exception) { }
    }

    public DataTable executarSQL(string comando_sql)
    {
        try
        {
            conectar();
            using (SqlDataAdapter adaptador = new SqlDataAdapter(comando_sql, conexao))
            {
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                return dt;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar SQL: " + ex.Message);
        }
        finally
        {
            desconectar();
        }
    }

    public int manutencaoDB_Parametros(SqlCommand comando)
    {
        int retorno = 0;
        try
        {
            comando.Connection = conectar();
            retorno = comando.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar comando com parâmetros: " + ex.Message);
        }
        finally
        {
            desconectar();
        }
        return retorno;
    }

    public DataTable exSQLParametros(SqlCommand comando)
    {
        try
        {
            comando.Connection = conectar();
            using (SqlDataReader dr = comando.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar consulta com parâmetros: " + ex.Message);
        }
        finally
        {
            desconectar();
        }
    }

    public object executarScalar(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = conectar();
            return cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao executar comando scalar: " + ex.Message);
        }
        finally
        {
            desconectar();
        }
    }


}
