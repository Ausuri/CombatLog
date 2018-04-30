using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CombatLog
{
	/// <summary>
	/// Summary description for GameLog.
	/// </summary>
	public class GameLog
	{
		private string _PathAlias;

		private string _FileName;
		private long _FileSize;
		private DateTime _CreationTime;
		private DateTime _SessionStartedDTM;
		private string _Listener;
		private string _LeafName;
		private StringBuilder _LogFileText = new StringBuilder();
		private string _UserComment;

		#region Event Stuff
		public delegate void ProcessingStartedEvent(object Sender, GameLog GameLogObj);
		public event ProcessingStartedEvent ProcessingStarted = null;

		public delegate void ProcessingUpdateEvent(object Sender, GameLog GameLogObj, int PositionInFile);
		public event ProcessingUpdateEvent ProcessingUpdate = null;

		public delegate void ProcessingCompleteEvent(object Sender, GameLog GameLogObj);
		public event ProcessingCompleteEvent ProcessingComplete = null;
		#endregion

		#region Regular Expression - Verify combat log file
//		private Regex VerifyCombatLog = new Regex(
//			@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(combat\)"
//			+ @".*?Your ",
		private Regex VerifyCombatLog = new Regex(
					@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(combat\)",
			RegexOptions.IgnoreCase
			| RegexOptions.Compiled
			);

		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Tue, Nov 28, 2006, 05:47:02 PM
		///  Using Expresso Version: 2.1.2150, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  \[ 
		///      [
		///      Space
		///  [LogDTM]: A named capture group. [\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d]
		///      \d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Space
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///   \] \(notify\) 
		///      Space
		///      ]
		///      Space
		///      (
		///      notify
		///      )
		///      Space
		///  [Message]: A named capture group. [.*?(?:scramble|jam|warp|dock|deactivates|commanded|out of control|Loading).*?\r]
		///      .*?(?:scramble|jam|warp|dock|deactivates|commanded|out of control|Loading).*?\r
		///          Any character, any number of repetitions, as few as possible
		///          Match expression but don't capture it. [scramble|jam|warp|dock|deactivates|commanded|out of control|Loading]
		///              Select from 8 alternatives
		///                  scramble
		///                  jam
		///                  warp
		///                  dock
		///                  deactivates
		///                  commanded
		///                  out of control
		///                      out
		///                      Space
		///                      of
		///                      Space
		///                  Loading
		///          Any character, any number of repetitions, as few as possible
		///          Carriage return
		///  
		///  
		/// </summary>
		private Regex NotifyMessage = new Regex(
			@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(notify\)"
			+ @" (?<Message>.*?(?:scramble|jam|warp|dock|deactivates|command"
			+ @"ed|out of control|Loading).*?\r)",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Compiled
			);

		#endregion
		
		#region Regular Expression - Extract File Headers
		private Regex RXChatHeaders = new Regex(
			@"Listener:(?<Listener>"
			+ @".*?)\x0d.*?Session started:(?<SessionStarted>.*?)\x0d",
			RegexOptions.IgnoreCase
			| RegexOptions.Multiline
			| RegexOptions.Singleline
			| RegexOptions.Compiled
			);
		#endregion

		#region Regular Expression Combat Log Parser
		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Thu, Jul 29, 2004, 04:11:36 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  [
		///  [LogDTM]: A named capture group. [\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d]
		///      \d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///  \]\(
		///      ]
		///      (
		///  [LogEntryType]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  \).*?Your
		///      )
		///      Any character, any number of repetitions, as few as possible
		///  [Weapon]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  [HitType]: A named capture group. [(?:barely misses|barely scratches|glances off|hits|is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)]
		///      Match expression but don't capture it. [barely misses|barely scratches|glances off|hits|is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on]
		///          Select from 9 alternatives
		///              barely misses
		///              barely scratches
		///              glances off
		///              hits
		///              is well aimed at
		///              lightly hits
		///              misses
		///              perfectly strikes
		///              places an excellent hit on
		///  [EnemyName]: A named capture group. [.*?]
		///      Any character, any number of repetitions, as few as possible
		///  Match expression but don't capture it. [, (inflicting|causing|doing|wrecking for) (?<Damage>\d*.\d*)|\x2e]
		///      Select from 2 alternatives
		///          , (inflicting|causing|doing|wrecking for) (?<Damage>\d*.\d*)
		///              ,
		///              [1]: A numbered capture group. [inflicting|causing|doing|wrecking for]
		///                  Select from 4 alternatives
		///                      inflicting
		///                      causing
		///                      doing
		///                      wrecking for
		///              [Damage]: A named capture group. [\d*.\d*]
		///                  \d*.\d*
		///                      Any digit, any number of repetitions
		///                      Any character
		///                      Any digit, any number of repetitions
		///          ASCII Hex 2e
		///  
		///  
		/// </summary>
        //private Regex EVECombatLog = new Regex(
        //    @"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogE"
        //    + @"ntryType>.*?)\).*?Your (?<Weapon>.*?) (?<HitType>(?:barely m"
        //    + @"isses|barely scratches|glances off|hits|is well aimed at|lig"
        //    + @"htly hits|misses|perfectly strikes|places an excellent hit o"
        //    + @"n)) (?<EnemyName>.*?)(?:, (inflicting|causing|doing|wrecking"
        //    + @" for) (?<Damage>\d*.\d*)|\x2e\r)",
        //    RegexOptions.IgnoreCase
        //    | RegexOptions.Compiled
        //    );
        private Regex EVECombatLog = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?(?:<color=0xff00ffff><b>)(?<Damage>.\d*)</b> <color=0x77ffffff><font size=10>to</font> <b><color=0xffffffff>(?<EnemyName>.*?)<\/b><font size=10><color=0x77ffffff>( - )(?<Weapon>.*)( - )(?<HitType>.[a-zA-Z]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		#endregion

		#region Regular Expression - Extract Warp To Messages
		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Mon, Aug 23, 2004, 05:53:04 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  [
		///  [LogDTM]: A named capture group. [\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d]
		///      \d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Any character
		///          Any digit
		///          Any digit
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///          :
		///          Any digit
		///          Any digit
		///  ]
		///  [1]: A numbered capture group. [(\(notify\) warping to (?<Location>.*?)\r)|(\(notify\) Autopilot jumping to (?<Location>.*?)\r)|(\(notify\) Autopilot warping to (?<Location>.*?)\r)|(\(none\) Jumping to Stargate \((?<Location>.*?)\).*?\r)]
		///      Select from 4 alternatives
		///          [2]: A numbered capture group. [\(notify\) warping to (?<Location>.*?)\r]
		///              \(notify\) warping to (?<Location>.*?)\r
		///                  (
		///                  notify
		///                  )
		///                  warpingto
		///                  [Location]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  Carriage return
		///          [3]: A numbered capture group. [\(notify\) Autopilot jumping to (?<Location>.*?)\r]
		///              \(notify\) Autopilot jumping to (?<Location>.*?)\r
		///                  (
		///                  notify
		///                  )
		///                  Autopilotjumpingto
		///                  [Location]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  Carriage return
		///          [4]: A numbered capture group. [\(notify\) Autopilot warping to (?<Location>.*?)\r]
		///              \(notify\) Autopilot warping to (?<Location>.*?)\r
		///                  (
		///                  notify
		///                  )
		///                  Autopilotwarpingto
		///                  [Location]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  Carriage return
		///          [5]: A numbered capture group. [\(none\) Jumping to Stargate \((?<Location>.*?)\).*?\r]
		///              \(none\) Jumping to Stargate \((?<Location>.*?)\).*?\r
		///                  (
		///                  none
		///                  )
		///                  JumpingtoStargate
		///                  (
		///                  [Location]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  )
		///                  Any character, any number of repetitions, as few as possible
		///                  Carriage return
		///  
		///  
		/// </summary>
		private Regex RXGetWarpMessages = new Regex(
			@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] ((\(notif"
			+ @"y\) warping to (?<Location>.*?)\r)|(\(notify\) Autopilot jum"
			+ @"ping to (?<Location>.*?)\r)|(\(notify\) Autopilot warping to"
			+ @" (?<Location>.*?)\r)|(\(none\) Jumping to Stargate \((?<Loca"
			+ @"tion>.*?)\).*?\r))",
			RegexOptions.IgnoreCase
			| RegexOptions.Singleline
			| RegexOptions.ExplicitCapture
			| RegexOptions.CultureInvariant
			| RegexOptions.Compiled
			);

		#endregion

		#region Regular Expression - Extract Attacker Data
		//  using System.Text.RegularExpressions;

		/// <summary>
		///  Regular expression built for C# on: Tue, Sep 14, 2004, 09:46:24 PM
		///  Using Expresso Version: 2.0.1548, http://www.ultrapico.com
		///  
		///  A description of the regular expression:
		///  
		///  Select from 2 alternatives
		///      \[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?:<color=0xffbb6600>)?((?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage\x2e\r)
		///          [
		///          [LogDTM]: A named capture group. [\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d]
		///              \d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any character
		///                  Any digit
		///                  Any digit
		///                  Any character
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  :
		///                  Any digit
		///                  Any digit
		///                  :
		///                  Any digit
		///                  Any digit
		///          ]
		///          (
		///          [LogEntryType]: A named capture group. [.*?]
		///              Any character, any number of repetitions, as few as possible
		///          )
		///          Match expression but don't capture it. [<color=0xffbb6600>], zero or one repetitions
		///              <color=0xffbb6600>
		///          [1]: A numbered capture group. [(?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage\x2e\r]
		///              (?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage\x2e\r
		///                  [MissileName]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  Match expression but don't capture it. [belonging to]
		///                      belonging to
		///                  [Attacker]: A named capture group. [.*?]
		///                      Any character, any number of repetitions, as few as possible
		///                  [HitType]: A named capture group. [aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting ]
		///                      Select from 8 alternatives
		///                          aims well at you, inflicting 
		///                          places an excellent hit on you, inflicting 
		///                          lightly hits you, doing 
		///                          hits you, doing 
		///                          barely scratches you, causing 
		///                          lands a hit on you which glances off, causing 
		///                          strikes you  perfectly, wrecking for 
		///                          heavily hits you, inflicting 
		///                  [Damage]: A named capture group. [\d*.\d*|no real]
		///                      Select from 2 alternatives
		///                          \d*.\d*
		///                              Any digit, any number of repetitions
		///                              Any character
		///                              Any digit, any number of repetitions
		///                          no real
		///                  damage
		///                  ASCII Hex 2e
		///                  Carriage return
		///      \[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for )(?<Damage>\d*.\d*|no real) damage\x2e\r
		///          [
		///          [LogDTM]: A named capture group. [\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d]
		///              \d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any character
		///                  Any digit
		///                  Any digit
		///                  Any character
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  Any digit
		///                  :
		///                  Any digit
		///                  Any digit
		///                  :
		///                  Any digit
		///                  Any digit
		///          ]
		///          (
		///          [LogEntryType]: A named capture group. [.*?]
		///              Any character, any number of repetitions, as few as possible
		///          )
		///          [Attacker]: A named capture group. [.*?]
		///              Any character, any number of repetitions, as few as possible
		///          [HitType]: A named capture group. [aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for ]
		///              Select from 7 alternatives
		///                  aims well at you, inflicting 
		///                  places an excellent hit on you, inflicting 
		///                  lightly hits you, doing 
		///                  hits you, doing 
		///                  barely scratches you, causing 
		///                  lands a hit on you which glances off, causing 
		///                  strikes you  perfectly, wrecking for 
		///          [Damage]: A named capture group. [\d*.\d*|no real]
		///              Select from 2 alternatives
		///                  \d*.\d*
		///                      Any digit, any number of repetitions
		///                      Any character
		///                      Any digit, any number of repetitions
		///                  no real
		///          damage
		///          ASCII Hex 2e
		///          Carriage return
		///  
		///  
		/// </summary>
        //public Regex AttackDataRX = new Regex(
        //    @"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogE"
        //    + @"ntryType>.*?)\) (?:<color=0xffbb6600>)?((?<MissileName>.*?) "
        //    + @"(?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at yo"
        //    + @"u, inflicting |places an excellent hit on you, inflicting |l"
        //    + @"ightly hits you, doing |hits you, doing |barely scratches yo"
        //    + @"u, causing |lands a hit on you which glances off, causing |s"
        //    + @"trikes you  perfectly, wrecking for |heavily hits you, infli"
        //    + @"cting )(?<Damage>\d*.\d*|no real) damage\x2e\r)|\[ (?<LogDTM"
        //    + @">\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?"
        //    + @")\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting"
        //    + @" |places an excellent hit on you, inflicting |lightly hits y"
        //    + @"ou, doing |hits you, doing |barely scratches you, causing |l"
        //    + @"ands a hit on you which glances off, causing |strikes you  p"
        //    + @"erfectly, wrecking for )(?<Damage>\d*.\d*|no real) damage\x2"
        //    + @"e\r",
        //    RegexOptions.IgnoreCase
        //    | RegexOptions.Multiline
        //    | RegexOptions.Compiled
        //    );

        public Regex AttackDataRX = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?)(?:\x20\x28)(?<MissileName>.*?)(\x29\x20)(?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\x2e\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r)|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29(\x2e\r|\r)))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?:<color=0xffbb6600>)?((?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
		#endregion

		public GameLog()
		{
			//
			// TODO: Add constructor logic here
			//

            //this._LogFileText = new StringBuilder();
            //this.ProcessingStarted = null;
            //this.ProcessingUpdate = null;
            //this.ProcessingComplete = null;
            //this.VerifyCombatLog = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(combat\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //this.NotifyMessage = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(notify\) (?<Message>.*?(?:scramble|jam|warp|dock|deactivates|commanded|out of control|Loading).*?\r)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //this.RXChatHeaders = new Regex(@"Listener:(?<Listener>.*?)\x0d.*?Session started:(?<SessionStarted>.*?)\x0d", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //this.EVECombatLog = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?(?:<color=0xff99bb00>)(?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|hit| is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:(inflicting|for |causing|doing|hits you for|wrecking for)(?:\x3C\x62\x3E)(?<Damage>\d*.\d*)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\x2e\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r)|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\x2e\r)))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?Your (?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:, (inflicting|causing|doing|wrecking for) (?<Damage>\d*.\d*)|\x2e\r)|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?(?:<color=0xff99bb00>)(?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|hit| is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:(inflicting|for |causing|doing|hits you for|wrecking for)(?:\x3C\x62\x3E)(?<Damage>\d*.\d*)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\x2e\r)))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //this.RXGetWarpMessages = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] ((\(notify\) warping to (?<Location>.*?)\r)|(\(notify\) Autopilot jumping to (?<Location>.*?)\r)|(\(notify\) Autopilot warping to (?<Location>.*?)\r)|(\(none\) Jumping to Stargate \((?<Location>.*?)\).*?\r))", RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            //this.AttackDataRX = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?)(?:\x20\x28)(?<MissileName>.*?)(\x29\x20)(?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\x2e\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r)|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29(\x2e\r|\r)))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?:<color=0xffbb6600>)?((?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
          
		}

		public GameLog(string FileName)
		{
            //this._LogFileText = new StringBuilder();
            //this.ProcessingStarted = null;
            //this.ProcessingUpdate = null;
            //this.ProcessingComplete = null;
            //this.VerifyCombatLog = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(combat\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //this.NotifyMessage = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \(notify\) (?<Message>.*?(?:scramble|jam|warp|dock|deactivates|commanded|out of control|Loading).*?\r)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //this.RXChatHeaders = new Regex(@"Listener:(?<Listener>.*?)\x0d.*?Session started:(?<SessionStarted>.*?)\x0d", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

            //this.EVECombatLog = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?(?:<color=0xff99bb00>)(?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|hit| is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:(inflicting|for |causing|doing|hits you for|wrecking for)(?:\x3C\x62\x3E)(?<Damage>\d*.\d*)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\x2e\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r)|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\x2e\r)))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?Your (?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:, (inflicting|causing|doing|wrecking for) (?<Damage>\d*.\d*)|\x2e\r)|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\).*?(?:<color=0xff99bb00>)(?<Weapon>.*?) (?<HitType>(?:barely misses|barely scratches|glances off|hits|hit| is well aimed at|lightly hits|misses|perfectly strikes|places an excellent hit on)) (?<EnemyName>.*?)(?:(inflicting|for |causing|doing|hits you for|wrecking for)(?:\x3C\x62\x3E)(?<Damage>\d*.\d*)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\x2e\r)))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //this.RXGetWarpMessages = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] ((\(notify\) warping to (?<Location>.*?)\r)|(\(notify\) Autopilot jumping to (?<Location>.*?)\r)|(\(notify\) Autopilot warping to (?<Location>.*?)\r)|(\(none\) Jumping to Stargate \((?<Location>.*?)\).*?\r))", RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            //this.AttackDataRX = new Regex(@"\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?)(?:\x20\x28)(?<MissileName>.*?)(\x29\x20)(?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\x2e\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r)|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |hits you for |strikes you  perfectly, wrecking for )(?:\x3C\x62\x3E)(?<Damage>\d*.\d*|no real)(\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65\r|\x3C\x2F\x62\x3E\x20\x64\x61\x6D\x61\x67\x65(?:\x20\x28)(?<HitType>.*?)(\x29(\x2e\r|\r)))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?:<color=0xffbb6600>)?((?<MissileName>.*?) (?:belonging to) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for |heavily hits you, inflicting )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r))|\[ (?<LogDTM>\d\d\d\d.\d\d.\d\d \d\d:\d\d:\d\d) \] \((?<LogEntryType>.*?)\) (?<Attacker>.*?) (?<HitType>aims well at you, inflicting |places an excellent hit on you, inflicting |lightly hits you, doing |hits you, doing |barely scratches you, causing |lands a hit on you which glances off, causing |strikes you  perfectly, wrecking for )(?<Damage>\d*.\d*|no real) damage(\x2e\r|\r)", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
          
			_FileName = FileName;
		}

		public string GetFile()
		{
			if ( _LogFileText.Length == 0 )
				LoadFile();

			return _LogFileText.ToString();
		}
		
		public string GetLogText()
		{
			string s;

			if ( _LogFileText.Length == 0 )
				LoadFile();

			s = _LogFileText.ToString();

			_LogFileText.Remove(0, _LogFileText.Length);

			return s;
		}

		private void LoadFile()
		{
			if ( _LogFileText.Length != 0 )
				return;

			if (!File.Exists(_FileName)) 
				throw new Exception("The file " + _FileName + " does not exist. Aborting load for this object");

			try
			{
				StreamReader sr = File.OpenText(_FileName);

				_LogFileText.Append(sr.ReadToEnd());

				sr.Close();

				//Debug.WriteLine("File: " + _LeafName + " loaded");
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool IsCombatLog()
		{
			if ( _LogFileText.Length == 0 )
				LoadFile();

			this.GetHeaders();

			if ( VerifyCombatLog.IsMatch(_LogFileText.ToString()) )
				return true;

			return false;
		}

		public void GetHeaders()
		{
			System.IFormatProvider format =
				new System.Globalization.CultureInfo("en-GB", true);

			string sessionStartedStr;

			if ( _LogFileText.Length == 0 )
				LoadFile();

			if ( RXChatHeaders.IsMatch(_LogFileText.ToString()) )
			{
				_Listener = RXChatHeaders.Match(_LogFileText.ToString()).Groups["Listener"].ToString().Trim();
				sessionStartedStr = RXChatHeaders.Match(_LogFileText.ToString()).Groups["SessionStarted"].ToString().Trim();

				//   Session started: 2004.04.28 00:05:12

				_SessionStartedDTM = DateTime.ParseExact(sessionStartedStr, "yyyy.M.d HH:mm:ss", format);
			}
			else
			{
				// throw new Exception("No GameLog headers in " + this._FileName);
			}
		}


		public LocationInfo.LocationCollection GetWarpMessages()
		{
			LocationInfo.LocationCollection locs = null;

			try
			{
				if ( _LogFileText.Length == 0 )
					LoadFile();

				locs = new LocationInfo.LocationCollection();
			}
			catch (Exception e)
			{
				Debug.WriteLine("A cache entry has disappeared from the filesystem" + e.ToString());
			}


			if ( RXGetWarpMessages.IsMatch(_LogFileText.ToString()) )
			{
				System.IFormatProvider format =
					new System.Globalization.CultureInfo("en-GB", true);

				foreach ( Match m in RXGetWarpMessages.Matches(_LogFileText.ToString()) )
				{
					LocationInfo.Location l = new LocationInfo.Location();

					//
					// The length check here is a bit of a hack. EVE Log files seen to sometimes contain 
					// truncated "Warping to" messages. This check attempts to filter them out as they 
					// are of no use.
					//

					l.LocationStr = m.Groups["Location"].ToString();

					if ( l.LocationStr.Length > 3 ) 
					{
						l.LogDTM = DateTime.ParseExact(m.Groups["LogDTM"].Value, "yyyy.M.d HH:mm:ss", format);
						l.Source = Utils.LogFile.GetLineFromPosition(this._FileName, m.Index);
						l.SourceFileName = this._FileName;

						locs.Add(l);
					}
				}

				return locs;
			}

			return null;
		}

		public AttackEntryCollection GetAttackEntries()
		{
			if ( _LogFileText.Length == 0 )
				LoadFile();

			AttackEntryCollection ac = new AttackEntryCollection();

			System.IFormatProvider format =
				new System.Globalization.CultureInfo("en-GB", true);

			string damageStr;

			double percentageProcessed = 0.0F;
			double currentPosition;
			double fileSize = Convert.ToDouble(this.FileSize);

//			if ( ProcessingStarted != null )
//				ProcessingStarted(this, this);

			foreach ( Match m in AttackDataRX.Matches(_LogFileText.ToString()) )
			{
				AttackEntry a = new AttackEntry();

				a.LogDTM			= DateTime.ParseExact(m.Groups["LogDTM"].Value, "yyyy.M.d HH:mm:ss", format);
				a.WeaponName		= m.Groups["MissileName"].Value.ToString();

				if ( a.WeaponName == "" )
					a.WeaponName = "Turret";

				a.HitDescription	= m.Groups["HitType"].Value.ToString();
				a.AttackerName		= m.Groups["Attacker"].Value.ToString();

				a.AttackerName = a.AttackerName.Replace("<color=0xffbb6600>","");

				a.PositionInFile = m.Index;

				currentPosition = Convert.ToDouble(a.PositionInFile) + Convert.ToDouble(m.Length);

				percentageProcessed = currentPosition / fileSize;

				// Debug.WriteLine("Percentage Processed: " + percentageProcessed.ToString("0%") + " [" + m.Index + "]");

//				if ( ProcessingUpdate != null )
//					ProcessingUpdate(this, this, c.PositionInFile);

				a.MatchStringLength = m.Length;
			
				damageStr = m.Groups["Damage"].Value.ToString(format);

				try
				{
					a.DamageCaused = Convert.ToDouble(damageStr,format);
				}
				catch
				{
					a.Missed = true;
				}

				ac.Add(a);
			}

//			if ( ProcessingComplete != null )
//				ProcessingComplete(this, this);

			_LogFileText.Remove(0, _LogFileText.Length); // Dispose of the text from memory
			return ac;
		}

		public CombatLogEntryCollection GetCombatEntries()
		{
			if ( _LogFileText.Length == 0 )
				LoadFile();

			if ( !EVECombatLog.IsMatch(_LogFileText.ToString()) )
			{
				_LogFileText.Remove(0, _LogFileText.Length);
				return null;
			}

			CombatLogEntryCollection cc = new CombatLogEntryCollection();

			System.IFormatProvider format =
				new System.Globalization.CultureInfo("en-GB", true);

			string damageStr;

			//Debug.WriteLine("About to parse combat log");
			//Debug.WriteLine("Combat log file size is: " + this.FileSize.ToString());

			double percentageProcessed = 0.0F;
			double currentPosition;
			double fileSize = Convert.ToDouble(this.FileSize);

			if ( ProcessingStarted != null )
				ProcessingStarted(this, this);

			string lastNotifyMessage = "";

			foreach ( Match m in NotifyMessage.Matches(_LogFileText.ToString()) )
			{
				if ( lastNotifyMessage != m.Groups["Message"].Value.ToString() )
				{
					CombatLogEntry c = new CombatLogEntry();
					c.IsNotifyMessage = true;
					c.LogDTM = DateTime.ParseExact(m.Groups["LogDTM"].Value, "yyyy.M.d HH:mm:ss", format);
					c.WeaponName = m.Groups["Message"].Value.ToString();
					c.WeaponName = c.WeaponName.Substring(0, c.WeaponName.Length-1);
					c.WeaponName = c.WeaponName.Replace("&lt;", "<");
					c.WeaponName = c.WeaponName.Replace("&gt;", ">");

					c.PositionInFile = m.Index;
					cc.Add(c);
					lastNotifyMessage = m.Groups["Message"].Value.ToString();
				}
			}

			foreach ( Match m in EVECombatLog.Matches(_LogFileText.ToString()) )
			{
				CombatLogEntry c = new CombatLogEntry();
				c.IsNotifyMessage = false;

				c.LogDTM = DateTime.ParseExact(m.Groups["LogDTM"].Value, "yyyy.M.d HH:mm:ss", format);
				c.WeaponName = m.Groups["Weapon"].Value.ToString();
				c.HitDescription = m.Groups["HitType"].Value.ToString();
				c.TargetName = m.Groups["EnemyName"].Value.ToString();

				// Players in an alliance are appearing in log files as Hurg[ER]<Alliance Name>
				// This is encoded in log file entries as &lt;Alliance Name&gt;

				c.TargetName = c.TargetName.Replace("&lt;", "<");
				c.TargetName = c.TargetName.Replace("&gt;", ">");

				//
				// The regular expression has problems extracting the target name without the "completely" bit on the end e.g.
				// "Your [foo] misses Sansha's Minion completely"
				//
				// Rather than make the regex even more complex than it currently is, just look for the word completely in the
				// target name and remove it manually.
				//
				if ( c.TargetName.IndexOf("completely") != -1 )
					c.TargetName = c.TargetName.Substring(0, c.TargetName.IndexOf("completely")-1);

				c.PositionInFile = m.Index;

				currentPosition = Convert.ToDouble(c.PositionInFile) + Convert.ToDouble(m.Length);

				percentageProcessed = currentPosition / fileSize;

                // Debug.WriteLine("Percentage Processed: " + percentageProcessed.ToString("0%") + " [" + m.Index + "]");

				if ( ProcessingUpdate != null )
					ProcessingUpdate(this, this, c.PositionInFile);

				c.MatchStringLength = m.Length;
			
				damageStr = m.Groups["Damage"].Value.ToString(format);

				try
				{
					c.DamageCaused = Convert.ToDouble(damageStr,format);
				}
				catch
				{
					c.Missed = true;
				}

				cc.Add(c);
			}

			if ( ProcessingComplete != null )
				ProcessingComplete(this, this);

			// _LogFileText.Remove(0, _LogFileText.Length); // Dispose of the text from memory
			return cc;
		}

		public string PathAlias
		{
			get { return _PathAlias; }
			set { _PathAlias = value; }
		}

		public string FileName
		{
			get { return this._FileName; }
			set { this._FileName = value; }
		}

		public long FileSize
		{
			get { return this._FileSize; }
			set { this._FileSize = value; }
		}

		public DateTime CreationTime
		{
			get { return this._CreationTime; }
			set { this._CreationTime = value; }
		}

		public string LeafName
		{
			get { return this._LeafName; }
			set { this._LeafName = value; }
		}

		public string Listener
		{
			get { return this._Listener; }
			set { this._Listener = value; }
		}

		public System.DateTime SessionStartedDTM
		{
			get { return this._SessionStartedDTM; }
			set { this._SessionStartedDTM = value; }
		}

		public string UserComment
		{
			get { return this._UserComment; }
			set { this._UserComment = value; }
		}
	}
}
