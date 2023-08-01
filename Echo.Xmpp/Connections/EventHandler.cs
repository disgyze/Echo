namespace Echo.Xmpp.Connections
{
    public delegate TResult EventHandler<TEventArgs, TResult>(object sender, TEventArgs e);
}