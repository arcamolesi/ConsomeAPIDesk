using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ConsomeAPIDesk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();
            aluno.nome = textBox1.Text;
            aluno.disciplina = textBox2.Text;
            aluno.nota1 = Convert.ToSingle(textBox3.Text);
            aluno.nota2 = Convert.ToSingle(textBox4.Text);

            if (aluno != null)
            {
                using (var client = new HttpClient())
                {
                    // client.BaseAddress = new Uri("https://localhost:7074/api/Produtos");
                    //HTTP POST
                    string url = "https://localhost:7254/api/Alunos";
                    aluno.id = 0;
                    var dataAsString = JsonConvert.SerializeObject(aluno);
                    var content = new StringContent(dataAsString);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var postTask = client.PostAsync(url, content);

                    postTask.Wait();


                    var result = postTask.Result;

                }
                button2_Click(null, null); 

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            IEnumerable<Aluno> alunos = null;


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7254/api/Alunos\r\n");

                //HTTP GET
                var responseTask = client.GetAsync("alunos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Aluno>>();
                    readTask.Wait();
                    alunos = readTask.Result;
                }
                else
                {
                    alunos = Enumerable.Empty<Aluno>();
                }
            }
            dataGridView1.DataSource = ""; 
            dataGridView1.DataSource = alunos;

        }
    }
}