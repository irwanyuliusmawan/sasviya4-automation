using k8s;
using NUnit.Framework;

namespace SASViya4Test
{
    public class SASViyaDeploymentTests : BaseClass
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1), Category("Playlist4")]
        [TestCase(TestName = "Check Status of Kubernetes Worker Nodes")]
        public void CheckNodeStatus()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var client = new Kubernetes(config);
            var lstNodes = client.ListNode();
            int iCount = 0;
            foreach (var node in lstNodes.Items)
            {
                iCount++;
            }
            Assert.IsTrue(iCount >= 3, "The Active Nodes are Greater then 3");
        }

        [Test, Order(2), Category("Playlist4")]
        [TestCase(TestName = "Check Pods are Running Successfully")]
        public void CheckPodStatus()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var client = new Kubernetes(config);
            var lstPods = client.ListNamespacedPod("sasviya4aws");
            var iCount = 0;
            foreach (var pod in lstPods.Items)
            {
                if (pod.Status.Phase == "Running")
                    iCount++;
            }
            Assert.IsTrue(iCount >= 120, "The Active Nodes are Greater then 3");
        }
    }
}