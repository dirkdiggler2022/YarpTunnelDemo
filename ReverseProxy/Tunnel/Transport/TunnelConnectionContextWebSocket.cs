using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Connections.Client;

namespace Yarp.ReverseProxy.Tunnel.Transport;

internal sealed class TunnelConnectionContextWebSocket : HttpConnection
{
    internal static async ValueTask<TrackLifetimeConnectionContext> ConnectAsync(
        // System.Net.Http.HttpMessageInvoker httpMessageInvoker,
        Uri uri,
        CancellationToken cancellationToken)
    {
        ClientWebSocket? underlyingWebSocket = null;
        var options = new HttpConnectionOptions
        {
            Url = uri,
            Transports = HttpTransportType.WebSockets,
            SkipNegotiation = true,
            WebSocketFactory = async (context, cancellationToken) =>
            {
                underlyingWebSocket = new ClientWebSocket();
                underlyingWebSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(5);
                await underlyingWebSocket.ConnectAsync(context.Uri, cancellationToken);
                return underlyingWebSocket;
            }
        };

        var connection = new TunnelConnectionContextWebSocket(options);
        await connection.StartAsync(TransferFormat.Binary, cancellationToken);
        connection._underlyingWebSocket = underlyingWebSocket;
        return new TrackLifetimeConnectionContext(connection);
    }

    private readonly CancellationTokenSource _cts = new();
    private WebSocket? _underlyingWebSocket;

    private TunnelConnectionContextWebSocket(HttpConnectionOptions options)
        : base(options, null)
    {
    }

    public override CancellationToken ConnectionClosed
    {
        get => _cts.Token;
        set { }
    }

    public override void Abort()
    {
        _cts.Cancel();
        _underlyingWebSocket?.Abort();
    }

    public override void Abort(ConnectionAbortedException abortReason)
    {
        _cts.Cancel();
        _underlyingWebSocket?.Abort();
    }

    public override ValueTask DisposeAsync()
    {
        // REVIEW: Why doesn't dispose just work?
        Abort();

        return base.DisposeAsync();
    }
}
