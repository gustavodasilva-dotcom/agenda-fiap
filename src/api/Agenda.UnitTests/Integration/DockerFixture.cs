using Docker.DotNet;
using Docker.DotNet.Models;

namespace Agenda.UnitTests.Integration
{
    public class DockerFixture : IDisposable
    {
        private DockerClient _dockerClient;
        private string _containerId;

        public DockerFixture() {
            _dockerClient = new DockerClientConfiguration().CreateClient();

            var createContainerResponse = _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters {
                Image = "sqlserver2019dev/mssql2019:latest",
                Env = new List<string>
                {
                    "ACCEPT_EULA=Y",
                    "SA_PASSWORD=YourStrong!Passw0rd" 
                },
                HostConfig = new HostConfig {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { "1433/tcp", new List<PortBinding> { new PortBinding { HostPort = "1433" } } }
                    },
                    PublishAllPorts = true 
                }
            }).GetAwaiter().GetResult();

            _containerId = createContainerResponse.ID;

            _dockerClient.Containers.StartContainerAsync(_containerId, new ContainerStartParameters()).GetAwaiter().GetResult();
        }

        public string GetConnectionString(string database) {
            return $"Server=localhost,1433;Database={database};User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";

        }

        public void Dispose() {
            _dockerClient.Containers.StopContainerAsync(_containerId, new ContainerStopParameters()).GetAwaiter().GetResult();
            _dockerClient.Containers.RemoveContainerAsync(_containerId, new ContainerRemoveParameters()).GetAwaiter().GetResult();
            _dockerClient.Dispose();
        }
    }
}

