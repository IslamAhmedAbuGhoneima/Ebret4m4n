/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      fontFamily: {
        arabic: ["Cairo", "normal"],
      },
      screens: {
        xl: "1440px",
      },
      container: {
        center: true,
        padding: "1rem",
        screens: {
          sm: "640px",
          md: "750px",
          lg: "970px",
          xl: "1200px",
        },
      },
      colors: {
        mainBg: {
          100: "#FCFCFC", //main white background... white
        },
        textColor: {
          100: "#121212", //text color....black
        },
        formBg: {
          100: "#24882805", //form background color....gray
        },
        primary: {
          //green
          btn: "#00712D", //button background color... green
          txt: "#319136", // green text exist in button...green
          num: "#46AC97", // text numeric data color.... mintgreen
        },
        secondary: {
          //blue
          btn: "#1E88E5", // doctor accept btn by A.Health..blue
          table: "#2473AB", //vaccine table body background.... blue
          chart: "#60D3BC", //chart color
        },
        error: {
          //red
          100: "#FE0000", //general vaccine table header background... red
        },
        third: {
          //gray
          table: "#00712D14", // vaccine table header background color....gray
          btn: "#12121266", // report side effect button bg..gray
          desc: "#12121299", //text description color... gray
          place: "#12121266", //form placeholder color....gray
        },
        male: {
          100: "#1E88E5", // male percentage color
        },
        female: {
          100: "#DE65DA", //female percentage color
        },
      },
    },
  },
  plugins: [],
};
