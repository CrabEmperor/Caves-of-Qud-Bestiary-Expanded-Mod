using System;
using System.Collections.Generic;
using System.Text;
using XRL.World;
using XRL.World.Parts;

#nullable disable
namespace XRL.Liquids
{
  [IsLiquid]
  [Serializable]
  public class LiquidIchor : BaseLiquid
  {
    public new const string ID = "ichor";
    [NonSerialized]
    public static List<string> Colors = new List<string>(2)
    {
      "W",
      "O"
    };

    public LiquidIchor()
      : base("ichor")
    {
      this.FlameTemperature = 400;
      this.VaporTemperature = 1200;
      this.Combustibility = 2;
      this.Fluidity = 5;
      this.Evaporativity = 1;
      this.Staining = 2;
      this.ThermalConductivity = 35;
      this.CirculatoryLossTerm = "bleeding";
      this.CirculatoryLossNoun = "bleed";
    }

    public override List<string> GetColors() => LiquidBlood.Colors;

    public override string GetColor() => "W";

    public override float GetValuePerDram() => 50f;

    public override bool Drank(
      LiquidVolume Liquid,
      int Volume,
      GameObject Target,
      StringBuilder Message,
      ref bool ExitInterface)
    {
      Message.Compound("It has a divine flavor of otherworldy metals.");
      return true;
    }

    public override string GetSmearedAdjective(LiquidVolume Liquid) => "{{W|ichor stained}}";

    public override string GetSmearedName(LiquidVolume Liquid) => "{{W|ichor stained}}";

    public override string GetStainedName(LiquidVolume Liquid) => "{{W|ichor}}";

    public override string GetName(LiquidVolume Liquid) => "{{W|ichor}}";

    public override string GetWaterRitualName() => "ichor";

    public override string GetAdjective(LiquidVolume Liquid) => "{{W|ichor stained}}";

    public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
    {
      if (!eRender.ColorsVisible)
        return;
      eRender.ColorString = "^W" + eRender.ColorString;
    }

    public override void BaseRenderPrimary(LiquidVolume Liquid)
    {
      Liquid.ParentObject.Render.ColorString = "&W^k";
      Liquid.ParentObject.Render.TileColor = "&W";
      Liquid.ParentObject.Render.DetailColor = "O";
    }

    public override void BaseRenderSecondary(LiquidVolume Liquid)
    {
      Liquid.ParentObject.Render.ColorString += "&W";
      if (!Liquid.ContainsLiquid("algae"))
        return;
      Liquid.ParentObject.Render.ColorString += "^C";
    }

    public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
    {
      if (!eRender.ColorsVisible)
        return;
      eRender.ColorString = "&W";
    }

    public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
    {
      if (!eRender.ColorsVisible)
        return;
      eRender.ColorString += "&W";
      if (!Liquid.ContainsLiquid("algae"))
        return;
      eRender.DetailColor += "C";
    }

    public override void RenderSmearPrimary(
      LiquidVolume Liquid,
      RenderEvent eRender,
      GameObject obj)
    {
      if (eRender.ColorsVisible)
        eRender.ColorString = "&W";
      base.RenderSmearPrimary(Liquid, eRender, obj);
    }

    public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
    {
      E.Add("might", 1);
    }
  }
}