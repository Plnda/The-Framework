namespace Bootstrap
{
    public class BasicBootstrap : Bootstrap
    {
        protected override void OnStart()
        {
            base.OnStart();

            if (Scene != null)
            {
                Scene.Boot(this);
            }
        }
    }
}
