import { Neurosity } from "@neurosity/sdk";
import osc from "osc";
import dotenv from "dotenv";
dotenv.config();

const {
    CROWN_ID,
    CROWN_EMAIL,
    CROWN_PWD,
    FOCUS_PORT = 9001 // port 9001 to avoid clashing with raw stream on 9000, override if needed
  } = process.env;

const neurosity = new Neurosity({
  deviceId: CROWN_ID
});

await neurosity.login({
  email:    CROWN_EMAIL,
  password: CROWN_PWD
});
console.log("✅ Logged into Crown cloud, streaming focus → UDP", FOCUS_PORT);

const tx = new osc.UDPPort({ metadata: true });
tx.open();

neurosity.focus().subscribe(({ probability }) => {
  tx.send(
    { address: "/focus", args: [{ type: "f", value: probability }] },
    "127.0.0.1",
    Number(FOCUS_PORT)
  );
});
