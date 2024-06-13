using Godot;
using Godot_XTension_Pack;

namespace Godot_XTension_Packs {

    [Tool]
    [GlobalClass]
    public abstract partial class IdentityResource : Resource {

        public string Id { get; private set; }

        private Guid? _guid;

        public Guid GetId() {
            _guid ??= Guid.Parse(Id);

            return _guid.Value;
        }

        public IdentityResource() {
            Id = Guid.NewGuid().ToString();
        }
    }

}
