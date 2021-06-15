using Puerts;
using PuertsStaticWrap;

namespace Base.Runtime
{
    public class JsEnvRuntime : Singleton<JsEnvRuntime>
    {
        public JsEnv JsEnv { get; private set; }

        public void Init()
        {
            JsEnv = new JsEnv(new JsLoaderRuntime());
            JsEnv.AutoUsing();
        }

        public void Shut()
        {
            JsEnv.Dispose();
            JsEnv = null;
        }


        public void Update()
        {
            JsEnv.Tick();
        }
    }
}
