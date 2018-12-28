@@delimiter(script, $____begin____, $_____end_____);

@doc
$____begin____

<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>fileTypes</key>
	<array>
		<string>dsl</string>
		<string>scp</string>
	</array>
	<key>name</key>
	<string>DSL</string>
	<key>patterns</key>
	<array>
		<dict>
			<key>include</key>
			<string>#expression</string>
		</dict>
	</array>
	<key>repository</key>
	<dict>
		<key>block</key>
		<dict>
			<key>begin</key>
			<string>\{</string>
			<key>beginCaptures</key>
			<dict>
				<key>0</key>
				<dict>
					<key>name</key>
					<string>meta.brace.curly.dsl</string>
				</dict>
			</dict>
			<key>end</key>
			<string>\}</string>
			<key>endCaptures</key>
			<dict>
				<key>0</key>
				<dict>
					<key>name</key>
					<string>meta.brace.curly.dsl</string>
				</dict>
			</dict>
			<key>name</key>
			<string>meta.block.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#expression</string>
				</dict>
			</array>
		</dict>
		<key>comment</key>
		<dict>
			<key>name</key>
			<string>comment.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#comment-block-doc</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#comment-block</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#comment-line</string>
				</dict>
			</array>
		</dict>
		<key>comment-block</key>
		<dict>
			<key>begin</key>
			<string>/\*</string>
			<key>end</key>
			<string>\*/</string>
			<key>name</key>
			<string>comment.block.dsl</string>
		</dict>
		<key>comment-block-doc</key>
		<dict>
			<key>begin</key>
			<string>/\*\*(?!/)</string>
			<key>end</key>
			<string>\*/</string>
			<key>name</key>
			<string>comment.block.documentation.dsl</string>
		</dict>
		<key>comment-line</key>
		<dict>
			<key>match</key>
			<string>(//|#).*$\n?</string>
			<key>name</key>
			<string>comment.line.dsl</string>
		</dict>
		<key>global-variable</key>
		<dict>
			<key>match</key>
			<string>@@[^\s]*</string>
			<key>name</key>
			<string>variable.language.dsl</string>
		</dict>
		<key>local-variable</key>
		<dict>
			<key>match</key>
			<string>@[^@][^\s]*</string>
			<key>name</key>
			<string>variable.other.dsl</string>
		</dict>
		<key>stack-variable</key>
		<dict>
			<key>match</key>
			<string>\$[^\s]*</string>
			<key>name</key>
			<string>variable.parameter.dsl</string>
		</dict>
		<key>delimiter</key>
		<dict>
			<key>match</key>
			<string>[,;]</string>
			<key>name</key>
			<string>constant.language.dsl</string>
		</dict>
		<key>control-statement</key>
		<dict>
			<key>match</key>

$_____end_____;

@include("keywords.xml");

@doc
$____begin____

			<key>name</key>
			<string>keyword.control.dsl</string>
		</dict>
		<key>function-call</key>
		<dict>
			<key>match</key>

$_____end_____;

@include("ids.xml");

@doc
$____begin____

			<key>name</key>
			<string>keyword.other.dsl</string>
		</dict>
		<key>numeric-literal</key>
		<dict>
			<key>match</key>
			<string>\b(?&lt;=[^$])((0(x|X)[0-9a-fA-F]+)|(0(o|O)[0-7]+)|(0(b|B)(0|1)+)|(([0-9]+(\.[0-9]+)?))([eE]([+-]?)[0-9]+(\.[0-9]+)?)?)\b</string>
			<key>name</key>
			<string>constant.numeric.dsl</string>
		</dict>
		<key>boolean-literal</key>
		<dict>
			<key>match</key>
			<string>\b(false|true)\b</string>
			<key>name</key>
			<string>constant.language.boolean.dsl</string>
		</dict>
		<key>literal</key>
		<dict>
			<key>name</key>
			<string>literal.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#numeric-literal</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#boolean-literal</string>
				</dict>
			</array>
		</dict>
		<key>qstring-double</key>
		<dict>
			<key>begin</key>
			<string>"</string>
			<key>end</key>
			<string>"|(?:[^\\\n]$)</string>
			<key>name</key>
			<string>string.double.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#string-character-escape</string>
				</dict>
			</array>
		</dict>
		<key>qstring-single</key>
		<dict>
			<key>begin</key>
			<string>'</string>
			<key>end</key>
			<string>\'|(?:[^\\\n]$)</string>
			<key>name</key>
			<string>string.single.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#string-character-escape</string>
				</dict>
			</array>
		</dict>
		<key>string</key>
		<dict>
			<key>name</key>
			<string>string.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#qstring-single</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#qstring-double</string>
				</dict>
			</array>
		</dict>
		<key>string-character-escape</key>
		<dict>
			<key>match</key>
			<string>\\(x\h{2}|[0-2][0-7]{,2}|3[0-6][0-7]?|37[0-7]?|[4-7][0-7]?|.|$)</string>
			<key>name</key>
			<string>constant.character.escape</string>
		</dict>
		<key>expression</key>
		<dict>
			<key>name</key>
			<string>meta.expression.dsl</string>
			<key>patterns</key>
			<array>
				<dict>
					<key>include</key>
					<string>#string</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#comment</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#literal</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#block</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#global-variable</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#local-variable</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#stack-variable</string>
				</dict>
				<dict>
				  <key>include</key>  
				  <string>#delimiter</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#control-statement</string>
				</dict>
				<dict>
					<key>include</key>
					<string>#function-call</string>
				</dict>
			</array>
		</dict>
	</dict>
	<key>scopeName</key>
	<string>source.dsl</string>
	<key>uuid</key>
	<string>45f8dea4-80ad-44b3-b424-7df58ccfc264</string>
</dict>
</plist>

$_____end_____;