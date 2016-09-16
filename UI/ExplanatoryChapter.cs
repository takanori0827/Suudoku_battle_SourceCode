using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* チャプターの説明 */
public class ExplanatoryChapter : MonoBehaviour {
	public GameObject TitlePage;
	public GameObject MainPage;
	public GameObject Chapter1;
	public GameObject Chapter2;
	public GameObject Chapter3;
	public GameObject Chapter4;
	public GameObject Chapter5;
	public GameObject ChapterTextPage;
	public GameObject SystemTextPage;
	public Text ChapterText;

	public void Start(){
		Chapter1.SetActive (false);
		Chapter2.SetActive (false);
		Chapter3.SetActive (false);
		Chapter4.SetActive (false);
		Chapter5.SetActive (false);
		ChapterTextPage.SetActive (false);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			TitlePage.SetActive (false);
			MainPage.SetActive (true);
		}
	}

	public void ShowText(){
		ChapterTextPage.SetActive (true);
	}

	public void Back(){
		MainPage.SetActive (true);
		Chapter1.SetActive (false);
		Chapter2.SetActive (false);
		Chapter3.SetActive (false);
		Chapter4.SetActive (false);
		Chapter5.SetActive (false);
		ChapterTextPage.SetActive (false);
		SystemTextPage.SetActive (false);
	}

	public void onClick_Chapter1(){
		Chapter1.SetActive (true);
	}

	public void onClick_Chapter2(){
		Chapter2.SetActive (true);
	}

	public void onClick_Chapter3(){
		Chapter3.SetActive (true);
	}

	public void onClick_Chapter4(){
		Chapter4.SetActive (true);
	}

	public void onClick_Chapter5(){
		Chapter5.SetActive (true);
	}

	public void System(){
		MainPage.SetActive (false);
		SystemTextPage.SetActive (true);
	}

	public void Chapter1_1(){
		ChapterText.text = "町が壊滅状態と聞き、町を目指して森を抜けようとしたとき、突然モンスターが襲ってきた。\nこの森のモンスター達はおとなしい性格で争いを好まないはずなのに突然襲ってくるなんておかしい・・・\nこれも町が壊滅した原因の一つなのか・・・？";
	}

	public void Chapter1_2(){
		ChapterText.text = "この突然変異について森のゴブリン族の長に聞いてみようと森の奥に入っていった。\nゴブリン族は知能が高く、人間としゃべることができる種族だ。\n奥に入っていくと大量のゴブリンが倒れており争った跡が残っていた。\nゴブリン族の長はその先にいた。\n話しかけようと近づいた時、突然遅い掛かってきた";
	}

	public void Chapter1_3(){
		ChapterText.text = "元の性格に戻ったゴブリン族の長はこの状況について話してくれた。\nある日突然、憎しみと憎悪が膨れ上がっていき些細なことで争いが起きる様になった。\nそして、誰かに操られているかのようにどんどんと仲間を殺し、森が荒れていってしまった。\n他のモンスター達も同じように大人しかった性格が一変していってしまった。\n近くの町が壊滅したのもこれが原因なのかもれないと語ってくれた。\n真実の石像なら何かわかるのではないかと言うことで真実の石像に向かうことになった\n\"真実の石像にたどり着くと、そこには一人の老婆が立っていた。\\n老婆は真実の石像に何かの呪文をかけると、真実の石像は突然動き始めこちらに襲い掛かってきた。\\nさらに虚偽の石像・判断の石像もそこには集まってきた。";
	}

	public void Chapter1_4(){
		ChapterText.text = "石像の脅威を避けると、老婆はこちらじっと見ていた。\n老婆に近づくと、自分を逃がさない様見えない壁を張った。\n老婆は洗脳にかかっていないものは脅威となると言い襲い掛かってきた。";
	}

	public void Chapter2_1(){
		ChapterText.text = "老婆を退け町に到着すると町はすっかり荒れ果ててしまっていた。\n町には人影が少し見れるがどれも昔の町の人間のオーラとは違うものを感じていた。\n１人の男が近づいて来たので話しかけると、帰ってきたのは飛ばしてきたナイフのみであった。";
	}

	public void Chapter2_2 (){
		ChapterText.text = "男は正気に戻り、この数日間で起こった町での出来事を教えてくれた。\n突然人々が殴り合い争いが起き始めた。座っていた人も突然隣の人に殴りかかったりと町全体が戦争の場所と化してしまった。\nなぜなのかはわからず、日々この様な生活が続き町のすべてのシステムはダウンしてしまい町は無残にも荒れ果ててしまった。\nこれは森と同じ出来事である。\n男とともにこの原因を突き止める為、まずは町を散策し始めた。すると突然人ではない者が襲い掛かってきた";
	}

	public void Chapter2_3 (){
		ChapterText.text = "襲い掛かってきたドラゴン族の兵士は襲い掛かってしまったことについて詫び、なぜ襲い掛かってしまったのかは不明と言うことだった。何かに操られているように自分の意識とは反する動きをしてしまったと。\nやはり何かがおかしい。世界が狂ってしまったかのように・・・。\nこの原因を探るため町の酒場に行くとにした。もしかしたら正気な者もいるかもしれないと\nしかし、酒場にも理性を失った者どもが争いをしていた。まずはこの争いを止めるのが先だ";
	}

	public void Chapter2_4(){
		ChapterText.text = "争いを止め、何があったのかを聞くとやはりみな同じように操られているかのように動いていたと話していた。\n噂によると橋を越えた先の城には正気な者が集まり解決策を練っていると噂が立っていた。\n噂を頼りにその城に行くことを決意し城の方の出口へ向かうと巨大な石像が置かれていた。\nその石像もまた真実の石像の様に阻んできた";
	}

	public void Chapter3_1(){
		ChapterText.text = "城に到着すると、城の前には一人の兵士が立っていた。先に進むには倒すしかないみたいだ";
	}

	public void Chapter3_2(){
		ChapterText.text = "城に入ると多くの者が倒れていた。また争いが起きていたみたいだ。その奥には一人の剣士が立っていた。";
	}

	public void Chapter3_3(){
		ChapterText.text = "剣士を倒し散策をしていると一枚の紙が落ちていた。「宇宙のどこかに人をおかしくしている根源があると」そう書かれた紙を拾った瞬間、城の外からデカい音がした。";
	}

	public void Chapter3_4(){
		ChapterText.text = "格闘家を倒し、外に出るとそこには巨大なドラゴンが迫ってきていた。もしかしたらこのドラゴンを使えば宇宙にいけるかもしれない。";
	}

	public void Chapter4_1(){
		ChapterText.text = "ドラゴンを使って宇宙に到着すると、ロボット達が襲ってきた。侵入者を排除するためのロボットだろう。";
	}

	public void Chapter4_2(){
		ChapterText.text = "ロボットを倒しても倒しても沸いてくる。スペースコロニーを走っていると他のロボットとは違うロボットが待っていた。";
	}

	public void Chapter4_3(){
		ChapterText.text = "全てのロボットが停止し走っていると、奥には一人の女性が立っていた。そこまで走っていると、止まっていたはずのロボットが襲い掛かってきた";
	}

	public void Chapter4_4(){
		ChapterText.text = "女性は巨大なロボットを起動させ、逃亡を図った。女性のスピードについていけない。この巨大なロボットを使えば追いつくかもしれない。";
	}

	public void Chapter5_1(){
		ChapterText.text = "女性は今の状況を作ったのは私だという。この状況を止める為最後の戦いを挑む。";
	}
		
}


