var http = require('http'), httpProxy = require('http-proxy');  
var url = require('url');
  
// 新建一个代理 Proxy Server 对象  
var proxy = httpProxy.createProxyServer({});  

// 捕获异常  
proxy.on('error', function (err, req, res) {  
  res.writeHead(500, {  
    'Content-Type': 'text/plain'  
  });  
  res.end('Something went wrong. And we are reporting a custom error message.');  
});  
  
// 另外新建一个 HTTP 80 端口的服务器，也就是常规 Node 创建 HTTP 服务器的方法。  
// 在每次请求中，调用 proxy.web(req, res config) 方法进行请求分发  
var server = http.createServer(function(req, res) {  
  // 在这里可以自定义你的路由分发  
  var pathName = url.parse(req.url).pathname;
  var host = req.headers.host, ip = req.headers['x-forwarded-for'] || req.connection.remoteAddress;  
  var target = 'page';

  console.log(url.parse(req.url));
	if(/\/api.*$/.test(pathName)) {
		target = "api";
	}

  console.log("[INFO] client ip:" + ip + ", host:" + host + ", target:" + target + " url: " + pathName);  

  if(target == 'api') {
    proxy.web(req, res, { target: 'http://localhost:4399' });  
  }else
  {
    //改成你的前端的端口
    proxy.web(req, res, { target: 'http://localhost:5001' });  
  }
});  
  
console.log("listening on port 3344")  
//通过这个端口访问项目
server.listen(3344); 