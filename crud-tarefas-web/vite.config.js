import { defineConfig } from 'vite';
import { defineConfig } from 'vitest/config';


export default defineConfig({
   server: {
    port: 4200
  },
  test: {
    globals: true,
    environment: 'jsdom',
    setupFiles: './src/setupTests.js', // Opcional, para setup inicial
  },
});
