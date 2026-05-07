import express from 'express';
import cors from 'cors';
import { join } from 'path';

const app = express();
const DIST_FOLDER = join(process.cwd(), 'dist/crud-tarefas-web/browser');

// Middleware
app.use(cors({ origin: '*' }));
app.use(express.json({ limit: '50mb' }));
app.use(express.urlencoded({ extended: true }));

// Serve arquivos estáticos
app.use(express.static(DIST_FOLDER, { maxAge: '1y' }));

// Proxy para Backend .NET (http://localhost:5000)
app.use('/api', async (req, res) => {
  const { fetch } = await import('node-fetch');
  const url = `http://localhost:5000${req.url}`;
  const response = await fetch(url, {
    method: req.method,
    headers: { 'Content-Type': 'application/json' },
    body: req.method !== 'GET' ? JSON.stringify(req.body) : undefined
  });
  const data = await response.json();
  res.json(data);
});

// SPA Fallback
app.get('*', (req, res) => {
  res.sendFile(join(DIST_FOLDER, 'index.html'));
});

const port = process.env['PORT'] || 4200;
app.listen(port, () => {
  console.log(`\n🚀 Angular CRUD funcionando:`);
  console.log(`   Frontend: http://localhost:${port}`);
  console.log(`   Backend:  http://localhost:5000`);
  console.log(`\n✅ Teste: http://localhost:${port}/tarefas\n`);
});
