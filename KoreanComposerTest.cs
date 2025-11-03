using System.Text;
using VirtualKeyboard.Input;
using VirtualKeyboard.Input.Korean.Composer;
using VirtualKeyboard.Input.Models;
using Xunit.Abstractions;

namespace VirtualKeyboardTest.Input
{
    public class KoreanComposerTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IME _ime;

        public KoreanComposerTest(ITestOutputHelper output)
        {
            _output = output;
            var composer = new KoreanComposer();
            _ime = new IME(composer);
        }

        #region 복합 종성 테스트

        [Fact]
        public void 케이스1_ㄱ_더하기_ㅅ_는_ㄳ()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            // Assert
            Assert.Empty(committed.ToString());  // 확정된 텍스트 없음
            Assert.Equal("ㄳ", result2.ComposingText);  // 조합 중: ㄳ
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}', 조합중: '{result2.ComposingText}'");
        }

        [Fact]
        public void 케이스2_ㄳ_더하기_ㅏ_는_ㄱ확정_사조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Equal("ㄱ", committed.ToString());  // ㄱ 확정
            Assert.Equal("사", result3.ComposingText);  // 조합 중: 사
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}', 조합중: '{result3.ComposingText}'");
        }

        [Fact]
        public void 케이스3_ㄳ_더하기_ㄱ_는_ㄳ확정_ㄱ조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Equal("ㄳ", committed.ToString());  // ㄳ 확정
            Assert.Equal("ㄱ", result3.ComposingText);  // 조합 중: ㄱ
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}', 조합중: '{result3.ComposingText}'");
        }

        [Fact]
        public void 케이스4_ㄱ_더하기_ㄴ_는_ㄱ확정_ㄴ조합_조합불가()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            // Assert
            Assert.Equal("ㄱ", committed.ToString());  // ㄱ 확정 (조합 불가)
            Assert.Equal("ㄴ", result2.ComposingText);  // 조합 중: ㄴ
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}', 조합중: '{result2.ComposingText}'");
        }

        #endregion

        #region 기본 한글 조합 테스트

        [Fact]
        public void 기본_초성_중성_조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            // Assert
            Assert.Empty(committed.ToString());  // 확정 없음
            Assert.Equal("가", result2.ComposingText);  // 조합 중: 가
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void 기본_초성_중성_종성_조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Empty(committed.ToString());  // 확정 없음
            Assert.Equal("각", result3.ComposingText);  // 조합 중: 각
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void 복합_종성_조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 값 (ㄱ + ㅏ + ㅂ + ㅅ)
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('ㅂ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            var result4 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(result4.CommittedText))
                committed.Append(result4.CommittedText);

            // Assert
            Assert.Empty(committed.ToString());  // 확정 없음
            Assert.Equal("값", result4.ComposingText);  // 조합 중: 값
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void 복합_중성_조합()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 과 (ㄱ + ㅗ + ㅏ)
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅗ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Empty(committed.ToString());  // 확정 없음
            Assert.Equal("과", result3.ComposingText);  // 조합 중: 과
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void Commit_호출_시_확정()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Commit();
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Equal("가", committed.ToString());  // 가 확정
            Assert.Empty(result3.ComposingText);  // 조합 중 없음
            Assert.False(_ime.IsComposing);
        }

        [Fact]
        public void Backspace_복합_중성_분해()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 과 입력 후 백스페이스
            _ime.Input('ㄱ');
            _ime.Input('ㅗ');
            var result1 = _ime.Input('ㅏ');  // 과
            var result2 = _ime.Backspace();   // 고

            // Assert
            Assert.Equal("고", result2.ComposingText);  // ㅘ → ㅗ
            Assert.Empty(result2.CommittedText);  // 확정 없음
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void Backspace_복합_종성_분해()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 값 입력 후 백스페이스
            _ime.Input('ㄱ');
            _ime.Input('ㅏ');
            _ime.Input('ㅂ');
            var result1 = _ime.Input('ㅅ');  // 값
            var result2 = _ime.Backspace();   // 갑

            // Assert
            Assert.Equal("갑", result2.ComposingText);  // ㅄ → ㅂ
            Assert.Empty(result2.CommittedText);  // 확정 없음
            Assert.True(_ime.IsComposing);
        }

        #endregion

        #region 실제 단어 입력 시나리오

        [Fact]
        public void 안녕하세요_입력()
        {
            // Arrange
            var committed = new StringBuilder();
            var composing = "";

            // Act & Process: 안
            ProcessInput('ㅇ', ref committed, ref composing);
            ProcessInput('ㅏ', ref committed, ref composing);
            ProcessInput('ㄴ', ref committed, ref composing);

            // Commit
            var commitResult = _ime.Commit();
            if (!string.IsNullOrEmpty(commitResult.CommittedText))
                committed.Append(commitResult.CommittedText);
            composing = commitResult.ComposingText;

            // Act & Process: 녕
            ProcessInput('ㄴ', ref committed, ref composing);
            ProcessInput('ㅕ', ref committed, ref composing);
            ProcessInput('ㅇ', ref committed, ref composing);

            // Commit
            commitResult = _ime.Commit();
            if (!string.IsNullOrEmpty(commitResult.CommittedText))
                committed.Append(commitResult.CommittedText);
            composing = commitResult.ComposingText;

            // Assert
            Assert.Equal("안녕", committed.ToString());
            Assert.Empty(composing);
            Assert.False(_ime.IsComposing);
        }

        private void ProcessInput(char ch, ref StringBuilder committed, ref string composing)
        {
            var result = _ime.Input(ch);
            if (!string.IsNullOrEmpty(result.CommittedText))
                committed.Append(result.CommittedText);
            composing = result.ComposingText;
        }

        #endregion

        #region 한글 아닌 입력 처리 테스트

        [Fact]
        public void 조합_중_숫자_입력_시_조합_확정()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 가 입력 후 1 입력
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);

            var result2 = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            var result3 = _ime.Input('1');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);

            // Assert
            Assert.Equal("가1", committed.ToString());  // 가 + 1 확정
            Assert.Empty(result3.ComposingText);  // 조합 중 없음
            Assert.False(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}'");
        }

        [Fact]
        public void 조합_중_영어_입력_시_조합_확정()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 한 입력 후 a 입력
            _ime.Input('ㅎ');
            _ime.Input('ㅏ');
            var result = _ime.Input('ㄴ');

            var result2 = _ime.Input('a');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);

            // Assert
            Assert.Equal("한a", committed.ToString());  // 한 + a 확정
            Assert.Empty(result2.ComposingText);
            Assert.False(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}'");
        }

        [Fact]
        public void 조합_중_특수문자_입력_시_조합_확정()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 가 입력 후 ! 입력
            _ime.Input('ㄱ');
            _ime.Input('ㅏ');

            var result = _ime.Input('!');
            if (!string.IsNullOrEmpty(result.CommittedText))
                committed.Append(result.CommittedText);

            // Assert
            Assert.Equal("가!", committed.ToString());  // 가 + ! 확정
            Assert.Empty(result.ComposingText);
            Assert.False(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}'");
        }

        [Fact]
        public void 조합_중_아님_숫자_입력_시_NoChange()
        {
            // Arrange & Act
            var result = _ime.Input('1');

            // Assert
            Assert.True(result.Success);  // NoChange는 success: true
            Assert.Equal(ECompositionAction.None, result.Action);  // NoChange는 Action.None
            Assert.Empty(result.CommittedText);
            Assert.Empty(result.ComposingText);
            Assert.False(_ime.IsComposing);

            _output.WriteLine($"NoChange 반환");
        }

        [Fact]
        public void 복잡한_입력_시나리오()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act: 안녕1하2세3요
            // 안
            var result = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            result = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            result = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 1
            result = _ime.Input('1');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 하
            result = _ime.Input('ㅎ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            result = _ime.Input('ㅏ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 2
            result = _ime.Input('2');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 세
            result = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            result = _ime.Input('ㅔ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 3
            result = _ime.Input('3');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            // 요
            result = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            result = _ime.Input('ㅛ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);

            var commitResult = _ime.Commit();
            if (!string.IsNullOrEmpty(commitResult.CommittedText))
                committed.Append(commitResult.CommittedText);

            // Assert
            Assert.Equal("안1하2세3요", committed.ToString());
            Assert.False(_ime.IsComposing);

            _output.WriteLine($"확정: '{committed}'");
        }

        #endregion

        #region 중성 조합 실패 테스트

        [Fact]
        public void 중성_조합_실패_ㅇㅑㅠ()
        {
            // Arrange
            var committed = new StringBuilder();
            var composing = "";

            _output.WriteLine("=== ㅇㅑㅠ 입력 시작 ===");

            // Act: ㅇ 입력
            var result1 = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);
            composing = result1.ComposingText;

            _output.WriteLine($"[1] ㅇ 입력:");
            _output.WriteLine($"    Success: {result1.Success}");
            _output.WriteLine($"    Action: {result1.Action}");
            _output.WriteLine($"    ComposingText: '{composing}'");
            _output.WriteLine($"    CommittedText: '{result1.CommittedText}'");
            _output.WriteLine($"    IsComposing: {_ime.IsComposing}");
            _output.WriteLine("");

            // Act: ㅑ 입력
            var result2 = _ime.Input('ㅑ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);
            composing = result2.ComposingText;

            _output.WriteLine($"[2] ㅑ 입력:");
            _output.WriteLine($"    Success: {result2.Success}");
            _output.WriteLine($"    Action: {result2.Action}");
            _output.WriteLine($"    ComposingText: '{composing}'");
            _output.WriteLine($"    CommittedText: '{result2.CommittedText}'");
            _output.WriteLine($"    IsComposing: {_ime.IsComposing}");
            _output.WriteLine("");

            // Act: ㅠ 입력 (조합 실패 예상)
            var result3 = _ime.Input('ㅠ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);
            composing = result3.ComposingText;

            _output.WriteLine($"[3] ㅠ 입력:");
            _output.WriteLine($"    Success: {result3.Success}");
            _output.WriteLine($"    Action: {result3.Action}");
            _output.WriteLine($"    ComposingText: '{composing}'");
            _output.WriteLine($"    CommittedText: '{result3.CommittedText}'");
            _output.WriteLine($"    IsComposing: {_ime.IsComposing}");
            _output.WriteLine("");

            _output.WriteLine($"=== 최종 결과 ===");
            _output.WriteLine($"확정된 텍스트: '{committed}'");
            _output.WriteLine($"조합 중인 텍스트: '{composing}'");

            // Assert - 수정 후 기대 동작
            _output.WriteLine("");
            _output.WriteLine($"=== 검증 ===");
            _output.WriteLine($"result3.Success가 true인가? {result3.Success}");
            _output.WriteLine($"result3.Action이 Input인가? {result3.Action == ECompositionAction.Input}");
            _output.WriteLine($"'야'가 확정되었는가? {committed.ToString() == "야"}");
            _output.WriteLine($"'ㅠ'가 조합 중인가? {composing == "ㅠ"}");

            // 수정 후: 조합 실패 시 현재 음절 확정 후 새 중성 입력
            Assert.True(result3.Success);
            Assert.Equal(ECompositionAction.Input, result3.Action);
            Assert.Equal("야", committed.ToString());  // "야" 확정
            Assert.Equal("ㅠ", composing);  // "ㅠ" 조합 중
            Assert.True(_ime.IsComposing);  // 새로운 조합 시작
        }

        [Fact]
        public void 중성_조합_실패_ㄱㅗㅠ()
        {
            // Arrange
            var committed = new StringBuilder();
            var composing = "";

            _output.WriteLine("=== ㄱㅗㅠ 입력 시작 ===");

            // Act: ㄱ 입력
            var result1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(result1.CommittedText))
                committed.Append(result1.CommittedText);
            composing = result1.ComposingText;

            _output.WriteLine($"[1] ㄱ 입력: ComposingText='{composing}', Success={result1.Success}, Action={result1.Action}");

            // Act: ㅗ 입력
            var result2 = _ime.Input('ㅗ');
            if (!string.IsNullOrEmpty(result2.CommittedText))
                committed.Append(result2.CommittedText);
            composing = result2.ComposingText;

            _output.WriteLine($"[2] ㅗ 입력: ComposingText='{composing}', Success={result2.Success}, Action={result2.Action}");

            // Act: ㅠ 입력 (ㅗ + ㅠ는 조합 불가)
            var result3 = _ime.Input('ㅠ');
            if (!string.IsNullOrEmpty(result3.CommittedText))
                committed.Append(result3.CommittedText);
            composing = result3.ComposingText;

            _output.WriteLine($"[3] ㅠ 입력: ComposingText='{composing}', Success={result3.Success}, Action={result3.Action}");
            _output.WriteLine($"확정된 텍스트: '{committed}', 조합 중: '{composing}'");

            // 수정 후: 조합 실패 시 "고" 확정 후 "ㅠ" 조합
            Assert.True(result3.Success);
            Assert.Equal(ECompositionAction.Input, result3.Action);
            Assert.Equal("고", committed.ToString());  // "고" 확정
            Assert.Equal("ㅠ", composing);  // "ㅠ" 조합 중
            Assert.True(_ime.IsComposing);
        }

        [Fact]
        public void 중성_조합_실패_후_자음_입력_ㅇㅑㅠㄴㅜ()
        {
            // Arrange
            var committed = new StringBuilder();
            var composing = "";

            _output.WriteLine("=== ㅇㅑㅠㄴㅜ 입력 시작 ===");
            _output.WriteLine("기대: '야ㅠ' 확정, '누' 조합 중");
            _output.WriteLine("");

            // ㅇㅑㅠ
            var result = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[1] ㅇ: committed='{committed}', composing='{composing}'");

            result = _ime.Input('ㅑ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[2] ㅑ: committed='{committed}', composing='{composing}'");

            result = _ime.Input('ㅠ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[3] ㅠ: committed='{committed}', composing='{composing}' (조합 실패로 '야' 확정)");

            // ㄴ 입력 (중성만 있는 상태에서 자음 입력)
            result = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[4] ㄴ: committed='{committed}', composing='{composing}' ('ㅠ' 확정, 'ㄴ' 조합 시작)");

            // ㅜ 입력
            result = _ime.Input('ㅜ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[5] ㅜ: committed='{committed}', composing='{composing}' ('ㄴ' + 'ㅜ' = '누')");
            _output.WriteLine("");

            // Assert
            Assert.Equal("야ㅠ", committed.ToString());  // "야ㅠ" 확정
            Assert.Equal("누", composing);  // "누" 조합 중
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"✅ 최종 결과: committed='{committed}', composing='{composing}'");
        }

        [Fact]
        public void 중성_조합_실패_후_모음_연속_입력_ㅇㅑㅠㅛㅣㄴ()
        {
            // Arrange
            var committed = new StringBuilder();
            var composing = "";

            _output.WriteLine("=== ㅇㅑㅠㅛㅣㄴ 입력 시작 ===");
            _output.WriteLine("기대: '야ㅠㅛㅣ' 확정, 'ㄴ' 조합 중");
            _output.WriteLine("");

            // ㅇㅑㅠ
            var result = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[1] ㅇ: committed='{committed}', composing='{composing}'");

            result = _ime.Input('ㅑ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[2] ㅑ: committed='{committed}', composing='{composing}'");

            result = _ime.Input('ㅠ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[3] ㅠ: committed='{committed}', composing='{composing}' (ㅑ+ㅠ 실패 → '야' 확정)");

            // ㅛ 입력 (중성만 있는 상태에서 중성 입력)
            result = _ime.Input('ㅛ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[4] ㅛ: committed='{committed}', composing='{composing}' (ㅠ+ㅛ 실패 → 'ㅠ' 확정)");

            // ㅣ 입력
            result = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[5] ㅣ: committed='{committed}', composing='{composing}' (ㅛ+ㅣ 실패 → 'ㅛ' 확정)");

            // ㄴ 입력
            result = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(result.CommittedText)) committed.Append(result.CommittedText);
            composing = result.ComposingText;
            _output.WriteLine($"[6] ㄴ: committed='{committed}', composing='{composing}' ('ㅣ' 확정, 'ㄴ' 조합 시작)");
            _output.WriteLine("");

            // Assert
            Assert.Equal("야ㅠㅛㅣ", committed.ToString());  // "야ㅠㅛㅣ" 확정
            Assert.Equal("ㄴ", composing);  // "ㄴ" 조합 중
            Assert.True(_ime.IsComposing);

            _output.WriteLine($"✅ 최종 결과: committed='{committed}', composing='{composing}'");
        }

        #endregion
    }
}

