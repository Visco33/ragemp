using GTANetworkAPI;
using System.Collections.Generic;
using System.IO;


public class BlipManager
{
    public struct BlipData
    {
        public uint id;
        public Vector3 position;
        public float scale;
        public int color;
        public string name;
        public bool shortRange;
        public uint dimension;
    }

    [Command("saveblips")]
    public void SaveAllBlipsCommand(Player player)
    {
        List<Blip> allBlips = NAPI.Pools.GetAllBlips();
        List<string> blipDataStrings = new List<string>();

        foreach (Blip blip in allBlips)
        {
            BlipData blipData = ExtractBlipData(blip);
            string formattedBlipData = FormatBlipData(blipData);
            blipDataStrings.Add(formattedBlipData);
        }

        File.WriteAllLines("blips.txt", blipDataStrings);
        player.SendChatMessage("All blips have been saved to blips.txt.");
    }

    private BlipData ExtractBlipData(Blip blip)
    {
        return new BlipData
        {
            id = blip.Sprite,
            position = blip.Position,
            scale = blip.Scale,
            color = blip.Color,
            name = blip.Name,
            shortRange = blip.ShortRange,
            dimension = blip.Dimension
        };
    }

    private string FormatBlipData(BlipData blipData)
    {
        return $"Main.CreateBlip(new Main.BlipData({blipData.id}, \"{blipData.name}\", new Vector3({blipData.position.X}, {blipData.position.Y}, {blipData.position.Z}), {blipData.scale}, {blipData.color}, {blipData.shortRange}));";
    }
}