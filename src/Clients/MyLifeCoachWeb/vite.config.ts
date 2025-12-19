import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    port: 9000,
    strictPort: true,
    hmr: {
      port: 9000,
    },
  },
});
