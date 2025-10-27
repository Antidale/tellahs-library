# A discord bot for [Free Enterprise](http://ff4fe.com/) 
Made to help supplement the information in [the wiki](https://wiki.ff4fe.com/doku.php)

# Commands
## Generally available commands
* `/recall boss`: It requires a single parameter of the boss name, and has an optional parameter to make the message ephemeral (which means that it's only visible to the caller, can be dismissed, and does disappear after a time). The bossName parameter has some flexibility with what you can use, but allows for no ambiguity (e.g. King would be insufficient, since that could be referring to Odin (Baron King) , the K/Q Eblan fight, or the Leviatan spot, but orbs will get you information about the CPU fight). Available globally and in DMs.
* `/recall racing` Provides information and links for common racing questions. It is available globally, as well as in DMs.
* `/recall search` Search a growing database for links to articles, images, and videos related to Free Enterprise. Available globally as well as in DMs.
* `/recall flag_interactions` Provides some extra information about a few tricky/subtle flag interactions. Available globally as well as in DMs.
* `/recall resistance` Provides information about weakness/resistance math in FFIV. Has optional parameter to focused information about monster Traits.
* `/recall pitfalls` Provides information about some common pitfalls newer runners fall into.
* `/recall item` Provides information about important consumable items. Info includes flag availability, guarantees, inclusion in starter kits.
* `/recall key-item-placement` Provides information for how Kmain, Kmoon, Ksummon, and Kmiab change the size of the KI pool, and how potential key item locations are chosen when non-main checks are enabled.
* `/recall forks` Provides a link to all public for repositories. Also provides site links and announcement posts for any forks that have had an announcement post in #code-central on the FE Workshop discord.
* `/recall learningway` Provides some summary information and links for Learningway, a series of seeds and videos to help people learn Free Enterprise
* `/recall metadata` Allows you to upload a patched FE seed to the bot to have it read the metadata and tell you the flags, seed, binary flags, and checksum icons for it. This replies in an ephemeral message to you only, so it's mostly just for some verification.
* `/recall patch` Allows you to upload a patched FE seed to the bot and have it recreate a patch page, which lets people share older seeds that can no longer be generated. Replies in two messages, both of them public. The first messaged is the metadata, and the second is the html file of the patch page.
* `/SelectPB2JFlagset` selects one of three possible choices for a Push B To Jump flagset. Used by the [Hap B Leap Year](https://docs.google.com/document/d/1uXWiiT6guhWD7DHNrujqH-UUVJVA_jEWY775w25l4qk) side tournament primarily.
* `/roll flags` Allows rolling seeds at the main site [ff4e.com](https://ff4fe.com/) or at [galeswift's](https://ff4fe.galeswift.com) hosted fork. You must select which site to use as the generator, and enter the flag string you want rolled. You may optionally include a seed value, or make the interaction ephemeral.
* `/roll preset` Allows rolling a selection of presets. There is no option for which site a preset generates at, since flagsets are generally either made for the main site, or have specific fork flags they want to use. You may optionally include a seed value, or make the interaction ephemeral.

## Tournament commands
These commands are available for servers that have opted-in to having them on their server. Contact me in the bot's discord if you'd like to have them enabled.
* `/tournament_administration createtournament` Creates a tournament in the given server. Currently only accessible by people with the Admin permission in a server. Generally accessible by just typing `/createtournament`
* `/tournament_administration openregistration` Sets a tournament's registration period to Open, overriding time set at tournament creation. Generally accessible by just typing `/openregistration`
* `/tournament_administration closeregistration` Closes the registration period for a touirnament. Generally accessible by just typing `/closeregistration`
* `/tournament register` Registers the calling user for a tournament in the server that has open registration. Users must provide the twitch account they'll be streaming game footage to, and can provide desired pronouns as well as an alias. Generally accessible by just typing `/register`
* `/tournament drop` Drops the calling user from a tournament in that server that they have entered. Generally accessible by just typing `/drop`

# Invite links
* [Discord Server](https://discord.gg/x95jN69Ggf)
* [Reference Google Doc](https://docs.google.com/document/d/1m_U90JG2t3Ze0fUFLMCzMSHZNYcdnIWrcr7RWAgtpBU/edit#heading=h.amzv5bujk9gc)

# Credit and Thanks
Many, many thanks to ScytheMarshall for the copyedit on the `/boss` command, and continued invauable help.

Thanks to SchalaKitty for the use of hir boss icons.

Thanks to Kirchin, and by extenstion rivers mccown and Inven for putting together the original [Underleveled Boss Strategies](https://docs.google.com/document/d/1Xw1vsN-OROShv4ZxPcStwJ1LsmFlPcZr3IIjOBSNEww/edit#heading=h.2iayie9keco) document