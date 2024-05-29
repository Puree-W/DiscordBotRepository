using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_app.Commands
{
    public class Hello:ModuleBase<SocketCommandContext>
    {
        [Command("Hi")]
        public async Task Greeting()
        {
            await ReplyAsync("Hello");
        }
    }
}
