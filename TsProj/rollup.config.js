import typescript from 'rollup-plugin-typescript2';

export default [ {
    input: "src/index.ts",
    output: { 
      file: "../Assets/Res/JavaScript/index.js.txt",
      format: "umd",
      sourcemap: true,
      name: "index",
      globals: {
        csharp: "csharp",
        puerts: "puerts"
      }
    },
    plugins: [
      typescript({
        tsconfig: 'tsconfig.json',
      })
    ],
    external: [
      "puerts",
      "csharp"
    ],
  }
]
