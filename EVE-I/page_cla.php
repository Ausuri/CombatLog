<?php

include $_SERVER[DOCUMENT_ROOT]."/dancer_config.php"; // Do not touch this line. :)

// A. RELAY INGAME (OPTIONAL) ----------------------------------------------------------------------------------

	/* If this URL is navigated to through the Eve-Online-Ingame-Browser calling the RelayIngame()-function of
	   the master will redirect to the page you set. If this function is not called, this page will not be
	   displayed at all in the ingame-browser. Syntax: $GLOBALS[MASTER]->RelayIngame(PathAndPage, Owner) where
	   "PathAndPage" is the complete path within the ingame-directory of this site plus the pagename and "Owner"
	   is your owner-directory within the ingame-directory. The deactivated example below would call
	   /ingame/dancer/orecalc/index.php instead of this page when an ingame-browser would hit this URL. */

	# $GLOBALS[MASTER]->RelayIngame("orecalc/index.php", "dancer"); // Deactivated Example

// -------------------------------------------------------------------------------------------------------------





// B. SET THE PAGE GLOBALS -------------------------------------------------------------------------------------

	/* The page globals need to be set before the page is drawn (which happens at the very end of the page
	   through the function $GLOBALS[MASTER]->Draw(). I like to have them at the beginning of my code, but
	   sometimes you generate the values somewhere below in the content area, so be free to set those
	   global page variables whereever you want. */

	$GLOBALS[MASTER]->SetMode("info"); // Set the mode of this page (info (default), forum or commerce)
	$GLOBALS[MASTER]->SetAdminLevel(0); // Adminlevel you need at least to see the contents of this page
	$GLOBALS[MASTER]->SetPageDev("hurg"); // Name of the coder/owner of this page
	$GLOBALS[MASTER]->SetBrowserTitle("EVE Combat Log Analyser"); // Browsertitle you want to see after the "EVE-I | "
	# $GLOBALS[MASTER]->CloseNavbarOverride(true); // Close left bar (override User settings, default: off)
	# $GLOBALS[MASTER]->CloseTabbarOverride(true); // Close right bar (override User settings, default: off)

// -------------------------------------------------------------------------------------------------------------





// C. OPTIONAL COMPUTE INCOMING (OPTIONAL) ---------------------------------------------------------------------

	/* Do you have forms in the page calling this one? The function "ListenTo" of the LISTENER-Object checks
	   if the variable you are listening to is set. If this is the case, the corresponing php-script is called.
	   The format is: $GLOBALS[LISTENER]->ListenTo(Name, Variable, Owner) where "Name" is an identifier for your-
	   self, "Variable" is the name of the variable you are listening to and "Owner" is your directory-name. If
	   variable "Variable" is set when your page is called (=if it is in the GET- or POST-varlist) then the file
	   home/[Owner]/codelet/listen_[Variable].php is called. */

	# $GLOBALS[LISTENER]->ListenTo("POLL", "incomingvote", "dancer"); // Deactivated Example

// -------------------------------------------------------------------------------------------------------------





// D. ADD NAVBAR TABS (OPTIONAL) -------------------------------------------------------------------------------

	/* In this area the left tabs are registered in the order you want them to appear on the navigation bar. The
	   default is illustrated in this template, but you can add/change/remove them as you need them. Be sure to
	   check if your settings will help the user or confuse him though. The syntax is
	   $GLOBALS[MASTER]->AddTabLeft(Identifier, Name, Owner, Adminlevel) where "Identifier" is any string that
	   helps you identifing the tab, "Name" is the unique name of your tab, "Owner" is your directory-name and
	   "Adminlevel" is the minimal Adminlevel the user has to have to see this tab (0 is for all users). If you
	   want a tab to be visible for registered users only, use "if (!$GLOBALS[VAR_GUEST])" before your call. */

	$GLOBALS[MASTER]->AddLeftTab("ID", "identity", "dancer", 0); // Add Identity Tab (on by default)
	$GLOBALS[MASTER]->AddLeftTab("NAV", "navigation", "dancer", 0); // Add Navigation Tab (on by default)
	$GLOBALS[MASTER]->AddLeftTab("HELP", "support", "dancer", 0); // Add Support Tab (on by default)
	# $GLOBALS[MASTER]->AddLeftTab("LINKS", "links", "dancer"); // Add Links Tab (off by default)

// -------------------------------------------------------------------------------------------------------------





// E. ADD TABBAR TABS (OPTIONAL) -------------------------------------------------------------------------------

	/* In this area the right tabs are registered in the order you want them to appear on the navigation bar. The
	   default is illustrated in this template, but you can add/change/remove them as you need them. Be sure to
	   check if your settings will help the user or confuse him though. The syntax is
	   $GLOBALS[MASTER]->AddTabRight(Identifier, Name, Owner, Adminlevel) where "Identifier" is any string that
	   helps you identifing the tab, "Name" is the unique name of your tab, "Owner" is your directory-name and
	   "Adminlevel" is the minimal Adminlevel the user has to have to see this tab (0 is for all users). If you
	   want a tab to be visible for registered users only, use "if (!$GLOBALS[VAR_GUEST])" before your call. */

	# $GLOBALS[MASTER]->AddRightTab("STAT", "serverstatus", "dancer"); // Add Server Status Tab (off by default)
	if (!$GLOBALS[VAR_GUEST]) $GLOBALS[MASTER]->AddRightTab("MAIL", "pm", "dancer", 0); // Add Mail Tab (on by default)
	# $GLOBALS[MASTER]->AddRightTab("POLL", "poll", "dancer"); // Add Poll Tab (off by default)
	# $GLOBALS[MASTER]->AddRightTab("JOY", "satisfaction", "dancer"); // Add Satisfaction Meter Tab (off by default)
	# $GLOBALS[MASTER]->AddRightTab("LAU", "latestactiveusers", "dancer"); // Add Latest Active Users Tab (off by default)

// -------------------------------------------------------------------------------------------------------------





// F. GENERATE CONTENT -----------------------------------------------------------------------------------------

	/* In this area you define the content (middle part) of your page. You add content to the variable $C. Please
	   note that any global variables that have not been set so far need to be set at latest in this section too.
	   There are a few functions in the MASTER-object that help you to generate/design your content. Those
	   functions currently are (see examples below):
	   1. DrawTitle(Pagetitle, Icon) - Draws a standard EVE-I page headline with the pagetitle and an icon
	   2. DrawContainer(Text, Align, box-BG, text-class) - Draws a standard EVE-I box around a text and aligns that text,
	      for a full list of color-codes and classes for the EVE-I site ask Dancer */

	// Example: Use DrawTitle() at the beginning of your conent
	$C.=$GLOBALS[MASTER]->DrawTitle("EVE Combat Log Analyser", "handheld"); // Draw the page title (Arguments: Text, Icon)

	// Example: Add some text to your content (without a box around it)
	$C.="<p>";

	$strTemp="
	<table border=0 cellpadding=5 cellspacing=2 width=100%>
	<tr>
		<td width=\"100%\" valign=\"top\">
<p><b>Introduction</b></p>

		<p>The
EVE Combat Log Analyser (CLA) was written with a single purpose in
mind, to provide me with easy means of analysing my EVE combat logs, in
particular, how much damage I'm doing, to whom and when.</p>
		<p>As
you probably know, the EVE Client generates logs for just about
everything that happens in the game, in particular, acts of aggression
(such as firing your weapon(s) at a target). These log files can be
found in the Program Files\CCP\EVE\capture\Gamelogs drectory. Stored as
plain text, they can be loaded into a text editor for for your viewing
pleasure. Unfortunately, this is not the ideal solution if you want to
see how much damage your shiny new 40M ISK 425mm Proto Gauss is doing
comprared with it's unnamed counterpart. Even fishing out the wrecking
hit entries can be quite a pain. Want to see how much damage a gun is
inflicting on average? No chance..</p>
<p>This is where the CLA comes in. By extracting all of the relevant
data from your combat log files, (date/time, weapon fired, target fired
on, type of hit scored and damage done) and presenting you with lists
that can be ordered, filtered and summarised, the CLA allows you to
instantly dissect, slice and dice the information whichever way you
like. It'll even draw graphs! And... if there's some other aggregation
that you'd like see that's not supported by the application, no probem,
just use the CLA to export the log file in csv format, load it into
something like Excel have your wicked way with it.</p>
		<p><b>Release Status</b></p>
		<p>Fairly safe to call the current releases alpha. It's quite
literally hot off the compiler and as such has been subjected to
very limited end-user testing. So, it may be fragile, it may be
functionally incomplete (inept even?), it may (but it's not likely to) even cause your
hair to fall out and your false teeth to corrode. However, now is the ideal time to exert some influence over
it's development. If there's some feature you would like to see added
or a change to the way it does something made, speak now while the to-do
list is short and my enthusiam is high. See the <strong>Comments &amp; Suggestions</strong> section below for details on how to do this.</p>

		<p><b>What does it do?</b></p>
		<p>Key Features</p>
		<ul>
			<li>Parses and extracts combat data from EVE Gamelog files</li>
			<li>Provides various breakdowns of damage information including:
			<ul>
				<li>Damage by weapon (or multiple weapons)
				</li><li>Damage by hit type (or multiple hit types)
				</li><li>Damage by target (or multiple targets)
			</li>

			</ul>
			</li><li>Provides aggregate damage data including:
			<ul>
				<li>Total damage by weapon(s), hit type(s), target(s)
				</li><li>Average damage by weapon(s), hit type(s), target(s)
				</li><li>Hit rates e.g. actual and percentage ratings for hit vs. misses
				</li><li>DoT (Damage over Time) is coming Soon&#8482;
			</li></ul>
			</li><li>It draws purdy pictures - Okay, maybe not pretty but does produce some basic graphs showing damage by weapon by hit type
			</li><li>Very basic integration with EVE-I's object explorer e.g. Targets and Weapons can be cross referenced in the OE
			</li><li>Combat
logs can be filtered by Character (useful if you have multiple
accounts/characters) and Age (Today, last 7 days, last Month etc)
hopefully making it easier to locate specific log files. See <strong>To-Do</strong> for further enhancements
			</li><li>Multiple log files open simultaneously for side by side comparison
		</li></ul>

		<p></p>
		<p><b>Exports</b></p>
		<p>As
mentioned above, there are bound to be situations where you want to
look at the data in a particular way that is not supported by the app.
In this case, combat logs can be exported in CSV format suitable for
import into a tool like Excel or even into a database of your choosing.</p>

		<p><b>I don't trust EVE utilities, they'll steal my login details and mutilate my Gerbil</b></p>
		<p>Truth
is, the vast majority are perfectly safe and trustworthy. However, this
tool does not require your EVE Login details, it does not require an
internet connection and it most certainly does not require your banking
details :). Let me re-iterate: you don't have to be logged in to EVE,
you don't have to be connected to the internet and you certainly don't
need to provide any login (or otherwise) details. The only time the app
will make use of an internet connection is when you request information
on a Target or Weapon in which case, it will open a web browser window
pointing at the EVE-I Object Explorer page. I may, if I ever get around
to it, put in a \"latest version check\" feature.</p>

		<p><b>To-Do aka Future Additions</b></p>

		<ul>
		<li>Filter log files by Weapon and Target name</li>
		<li>DoT analysis</li>
		<li>Favourites
list for log files and log file entries - for keeping an organised
record of a specific battle (log file) or wrecking shot (log file entry)</li>
		<li>Ability
to annotate log files - The ability to add notes to a log file,
possibly useful for recording key information such as installed
modules, battle situation, location etc.</li>
		<li>Extract top damage shots - produce a list of the highest damage dealing shots for every weapon across all of your log files</li>

		</ul>
		<p><b>Limitations</b></p>
The combat information provided in EVE log files is pretty limited. It
is not, for example, possible to distinguish between multiple instances
of the same weapon type i.e. If you have 4x425 Rails and 2x250's I
can't tell which of the 425's or 250's fired a particular shot (it may
be possible to infer some of this based on RoF vs. shot time but let's
no go there just yet). In the same way, it is not possible to
distinguish between multiple instances of the same target type e.g. I
may attack and kill 5 Sansha's Butchers but this is impossible to
determine from the log file. And finally, the log file does not contain
information on which modules were fitted/active during the fight so
there is no way for me to tell how many damage/tracking/cap mods you
were using at a particular point in time.<p></p>
Applying pressure to CCP to augment the log file output with this
information along with weapon and target instance ID's would, in my
somewhat biased opinion, be a Good Thing&#8482; (although, in fairness, I'm
not sure that they've ever been asked to do this before). :) <p><b>Comments &amp; Suggestions</b></p>
		<p>Please post any comments and suggestions on the <a href=\"http://www.eve-i.com/forum/postlist.php?Cat=&Board=tool13\">forum</a>.
I do actually play EVE quite frequently so if you run into me (Hurg),
remember that when I shoot you, I'm just collecting more data to test
the app so stay nice and still and don't shoot back - you'll be helping
development... honest :)</p>

		<p><b>Downloading</b></p>
		<p>You can download the latest version using the appropriately named <i>Download EVE Combat Log Analyser</i> link at the bottom of this page. However, this application <b>requires the Microsoft .NET Framework 1.1</b> to run. Without this, the installer will just barf. You have been warned! Use the links below to find out about and download the .NET Framework</P>
<ul>
<li><a href=\"http://msdn.microsoft.com/netframework/technologyinfo/default.aspx\">Microsoft .NET Framework Home Page</a></li>
<li><a href=\"http://msdn.microsoft.com/netframework/downloads/framework1_1/\">Download .NET Framework 1.1 from microsoft.com</a></li>
</ul>
		
		<p><b>thanks to</b></p>
		<p>
		</p><ul>
			<li>CCP for obvious reasons</li>
			<li>The EVE-I team for providing such a supremely excellent site</li>
			<li>\"Mister C\" for testing and ideas</li>

			<li>Islay and Megacyte (EVE Character Manager and EVE Skill Monitor respective authors) for producing such excellent tools.</li>
		</ul>

		</td>
		<td class=titletext valign=top bgcolor=\"#DFDFDF\" align=\"center\"><b>Screenshots</b><br><br>
		<p><a href=\"".$GLOBALS[VAR_HOME]."/hurg/media/main1.png\" target=_blank\"><img src=\"".$GLOBALS[VAR_HOME]."/hurg/media/main1_thumb.gif\" border=\"0\"></a><br>Main Screen</p>
		<p><a href=\"".$GLOBALS[VAR_HOME]."/hurg/media/main2.png\" target=_blank\"><img src=\"".$GLOBALS[VAR_HOME]."/hurg/media/main2_thumb.gif\" border=\"0\"></a><br>Filtering</p>
	</tr>
	</table>";

	$C.="<p>".$GLOBALS[MASTER]->DrawContainer($strTemp);

	$strTemp="<a class=navi href=\"".$GLOBALS[VAR_HOME]."/hurg/setup_cla_001c.zip\"><li>Download EVE Combat Log Analyser</a><br>";
	$strTemp.="<a class=navi href=\"".$GLOBALS[VAR_FORUMURL]."/postlist.php?Cat=&Board=tool13\"><li>EVE Combat Log Analyser Forum</a>";
	$C.="<p>".$GLOBALS[MASTER]->DrawContainer($strTemp,"","2","color8");


// -------------------------------------------------------------------------------------------------------------





// G. DRAW PAGE START ------------------------------------------------------------------------------------------------

	/* Finally you call the SetContent()-function in the MASTER.object to set your freshly generated page content and
	   Draw() to actually generate the output. Note that in the Draw()-function an exit will occur, so everything you
	   want to set, write to the database, etc. must happen before your call of Draw(). */

	$GLOBALS[MASTER]->SetContent($C."<p>&nbsp;</p>");
	$GLOBALS[MASTER]->Draw();

// -------------------------------------------------------------------------------------------------------------
?>
