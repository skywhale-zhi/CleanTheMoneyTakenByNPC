using Terraria;
using TerrariaApi.Server;

namespace CleanTheMoneyTakenByNPC
{
    [ApiVersion(2, 1)]
    public partial class Class1 : TerrariaPlugin
    {
        public override string Author => "z枳";

        public override string Description => "清空敌怪捡到的钱，避免原版刷钱bug";

        public override string Name => "CleanTheMoneyTakenByNPC";

        public override Version Version => new Version(1, 0, 0, 0);

        public Class1(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.NpcAIUpdate.Register(this, OnNPCUpdate);
        }

        private void OnNPCUpdate(NpcAiUpdateEventArgs args)
        {
            if(args.Npc.active && args.Npc.extraValue > 0)
            {
                args.Npc.extraValue = 0;
                NetMessage.SendData(92, -1, -1, null, args.Npc.whoAmI, 0, args.Npc.position.X, args.Npc.position.Y, 0, 0, 0);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.NpcAIUpdate.Deregister(this, OnNPCUpdate);
            }
            base.Dispose(disposing);
        }
    }
}