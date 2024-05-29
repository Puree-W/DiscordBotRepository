using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Test_app.Commands
{   public class Randfunc : ModuleBase<SocketCommandContext>
    {
        [Command("rand")]
        public async Task Rand(params string[] userstring)
        {
            if (userstring.Length == 0)
            {
                await ReplyAsync("จะสุ่มเหี้ยไรล่ะ");
                return;
            }

            Random rnd = new Random();
            var choice = userstring[rnd.Next(userstring.Length)];
            await ReplyAsync($"ฉันขอเลือก!: {choice}");
        }
    }
}
