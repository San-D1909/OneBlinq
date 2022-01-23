using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Out
{
	public class PluginBundleOutput
	{
		public PluginBundleOutput()
        {

        }

		public PluginBundleOutput(PluginBundleModel pluginBundle)
        {
			this.Id = pluginBundle.Id;
			this.BundleName = pluginBundle.BundleName;
			this.BundleDescription = pluginBundle.BundleDescription;
			this.StripeProductId = pluginBundle.StripeProductId;
        }

		public PluginBundleOutput(PluginBundleModel pluginBundle, IEnumerable<PluginModel> plugins): this(pluginBundle)
		{
			this.Plugins = plugins;
		}

		public PluginBundleOutput(PluginBundleModel pluginBundle, IEnumerable<PluginModel> plugins, IEnumerable<UserModel> users): this(pluginBundle, plugins)
		{
			this.Users = users;
		}

		public int Id { get; set; }
		public string BundleName { get; set; }
		public string BundleDescription { get; set; }
		public string StripeProductId { get; set;}
		public IEnumerable<UserModel> Users { get; set; }
		public IEnumerable<PluginModel> Plugins { get; set; }
		public PluginBundleImageModel Image { get; set; }
	}

}
