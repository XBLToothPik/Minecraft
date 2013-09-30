using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Methods;
using MC.Save.Structs.UncompressedFile;
using MC.Save.Structs.Editing.Player.Structs;
namespace MC.Save.Structs.Editing.Player
{
    public class Player
    {
        private UFBodyEntry playerEntry;
        private Bundle.BundleUtils.BUMarkedBundles bundle;
        private bool loaded = false;

        public string Name { get; set; }
        public string ID { get; set; }

        public Data PlayerBaseData { get; set; }


        public Player(ref UFBodyEntry playerEnt, Bundle.BundleUtils.BUMarkedBundles bundle, bool load)
        {
            this.playerEntry = playerEnt;

            int datIndex = playerEnt.Name.IndexOf(".dat");
            int playersIndex = playerEnt.Name.IndexOf("players/") + 8;
            this.ID = playerEnt.Name.Substring(playersIndex, datIndex - 8);

            this.Name = GetKnownPlayerGamertag(playerEnt.Name, bundle);
            if (load)
              this.LoadStuff(playerEntry.GetStream());
        }

        public void Save()
        {
            loaded = false;
        }
        public void Load()
        {
            if (!loaded)
                LoadStuff(playerEntry.GetStream());
        }

        private void LoadStuff(Stream playerStream)
        {
            loaded = true;
            System.Windows.Forms.MessageBox.Show("F");
        }
        private void SaveStuff(Stream OutStream)
        {

        }

        private string GetKnownPlayerGamertag(string entName, Bundle.BundleUtils.BUMarkedBundles Bundles)
        {
            int datIndex = entName.IndexOf(".dat");
            int playersIndex = entName.IndexOf("players/") + 8;
            entName = entName.Substring(playersIndex, datIndex - 8);


            for (int i = 0; i < Bundles.Body.Bundles.Count; i++)
            {
                if (entName == Bundles.Body.Bundles[i].Description)
                    return Bundles.Body.Bundles[i].NickName;
            }
            return entName;
        }
    }
}
