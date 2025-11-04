using System.Text;
using VirtualKeyboard.Input;
using VirtualKeyboard.Input.Korean.Composer;
using VirtualKeyboard.Input.Models;
using Xunit.Abstractions;

namespace VirtualKeyboardTest.Input
{
    public class CheonjinComposerTest
    {
        private readonly ITestOutputHelper _output;
        private readonly IME _ime;

        public CheonjinComposerTest(ITestOutputHelper output)
        {
            _output = output;
            var composer = new CheonjinComposer();
            _ime = new IME(composer);
        }

        #region 자음 순환 테스트

        [Fact]
        public void 자음순환_ㄱ_ㅋ_ㄲ_순환()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㄱ');
            var result3 = _ime.Input('ㄱ');

            // Assert
            Assert.Equal("ㄱ", result1.ComposingText);
            Assert.Equal("ㅋ", result2.ComposingText);
            Assert.Equal("ㄲ", result3.ComposingText);

            _output.WriteLine($"ㄱ → ㅋ → ㄲ 순환 성공");
        }

        [Fact]
        public void 자음순환_ㄴ_ㄹ_순환()
        {
            // Act
            var result1 = _ime.Input('ㄴ');
            var result2 = _ime.Input('ㄴ');
            var result3 = _ime.Input('ㄴ');

            // Assert
            Assert.Equal("ㄴ", result1.ComposingText);
            Assert.Equal("ㄹ", result2.ComposingText);
            Assert.Equal("ㄴ", result3.ComposingText);  // 다시 순환

            _output.WriteLine($"ㄴ → ㄹ → ㄴ 순환 성공");
        }

        [Fact]
        public void 자음순환_ㅅ_ㅎ_ㅆ_순환()
        {
            // Act
            var result1 = _ime.Input('ㅅ');
            var result2 = _ime.Input('ㅅ');
            var result3 = _ime.Input('ㅅ');

            // Assert
            Assert.Equal("ㅅ", result1.ComposingText);
            Assert.Equal("ㅎ", result2.ComposingText);
            Assert.Equal("ㅆ", result3.ComposingText);

            _output.WriteLine($"ㅅ → ㅎ → ㅆ 순환 성공");
        }

        [Fact]
        public void 자음순환_ㅇ_ㅁ_순환()
        {
            // Act
            var result1 = _ime.Input('ㅇ');
            var result2 = _ime.Input('ㅇ');
            var result3 = _ime.Input('ㅇ');

            // Assert
            Assert.Equal("ㅇ", result1.ComposingText);
            Assert.Equal("ㅁ", result2.ComposingText);
            Assert.Equal("ㅇ", result3.ComposingText);  // 다시 순환

            _output.WriteLine($"ㅇ → ㅁ → ㅇ 순환 성공");
        }

        #endregion

        #region 기본 모음 조합 테스트

        [Fact]
        public void 모음조합_ㅣ_ㆍ_는_ㅏ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅣ');
            var result3 = _ime.Input('ㆍ');

            // Assert
            Assert.Equal("가", result3.ComposingText);

            _output.WriteLine($"ㄱ + ㅣ + ㆍ = 가");
        }

        [Fact]
        public void 모음조합_ㆍ_ㅣ_는_ㅓ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㆍ');
            var result3 = _ime.Input('ㅣ');

            // Assert
            Assert.Equal("거", result3.ComposingText);

            _output.WriteLine($"ㄱ + ㆍ + ㅣ = 거");
        }

        [Fact]
        public void 모음조합_ㆍ_ㅡ_는_ㅗ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㆍ');
            var result3 = _ime.Input('ㅡ');

            // Assert
            Assert.Equal("고", result3.ComposingText);

            _output.WriteLine($"ㄱ + ㆍ + ㅡ = 고");
        }

        [Fact]
        public void 모음조합_ㅡ_ㆍ_는_ㅜ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅡ');
            var result3 = _ime.Input('ㆍ');

            // Assert
            Assert.Equal("구", result3.ComposingText);

            _output.WriteLine($"ㄱ + ㅡ + ㆍ = 구");
        }

        [Fact]
        public void 모음조합_ㅡ_ㅣ_는_ㅢ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅡ');
            var result3 = _ime.Input('ㅣ');

            // Assert
            Assert.Equal("긔", result3.ComposingText);

            _output.WriteLine($"ㄱ + ㅡ + ㅣ = 긔");
        }

        #endregion

        #region 3중 모음 조합 테스트

        [Fact]
        public void 모음조합_ㅣ_ㆍ_ㆍ_는_ㅑ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅣ');
            var result3 = _ime.Input('ㆍ');
            var result4 = _ime.Input('ㆍ');

            // Assert
            Assert.Equal("갸", result4.ComposingText);

            _output.WriteLine($"ㄱ + ㅣ + ㆍ + ㆍ = 갸");
        }

        [Fact]
        public void 모음조합_ㆍ_ㆍ_ㅣ_는_ㅕ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㆍ');
            var result3 = _ime.Input('ㆍ');
            var result4 = _ime.Input('ㅣ');

            // Assert
            Assert.Equal("겨", result4.ComposingText);

            _output.WriteLine($"ㄱ + ㆍ + ㆍ + ㅣ = 겨");
        }

        [Fact]
        public void 모음조합_ㆍ_ㅡ_ㅣ_ㆍ_는_ㅘ()
        {
            // Act - ㆍ + ㅡ → ㅗ, ㅗ + ㅣ → ㅚ, ㅚ + ㆍ → ㅘ
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㆍ');
            var result3 = _ime.Input('ㅡ');  // 고
            var result4 = _ime.Input('ㅣ');  // 괴
            var result5 = _ime.Input('ㆍ');  // 과

            // Assert
            Assert.Equal("과", result5.ComposingText);

            _output.WriteLine($"ㄱ + ㆍ + ㅡ + ㅣ + ㆍ = 과");
        }

        [Fact]
        public void 모음조합_ㅡ_ㆍ_ㆍ_ㅣ_는_ㅝ()
        {
            // Act - ㅡ + ㆍ → ㅜ, ㅜ + ㆍ → ㅠ, ㅠ + ㅣ → ㅝ
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅡ');
            var result3 = _ime.Input('ㆍ');  // 구 (ㅜ)
            _output.WriteLine($"result3: {result3.ComposingText}");
            Assert.Equal("구", result3.ComposingText);

            var result4 = _ime.Input('ㆍ');  // 규 (ㅠ)
            _output.WriteLine($"result4: {result4.ComposingText}");
            Assert.Equal("규", result4.ComposingText);

            var result5 = _ime.Input('ㅣ');  // 궈 (ㅝ)
            _output.WriteLine($"result5: {result5.ComposingText}");
            Assert.Equal("궈", result5.ComposingText);

            _output.WriteLine($"ㄱ + ㅡ + ㆍ + ㆍ + ㅣ = 궈");
        }

        [Fact]
        public void 모음조합_ㆍ_ㆍ_ㅡ_는_ㅛ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㆍ');
            var result3 = _ime.Input('ㆍ');
            var result4 = _ime.Input('ㅡ');

            // Assert
            Assert.Equal("교", result4.ComposingText);

            _output.WriteLine($"ㄱ + ㆍ + ㆍ + ㅡ = 교");
        }

        [Fact]
        public void 모음조합_ㅡ_ㆍ_ㆍ_는_ㅠ()
        {
            // Act
            var result1 = _ime.Input('ㄱ');
            var result2 = _ime.Input('ㅡ');
            var result3 = _ime.Input('ㆍ');
            var result4 = _ime.Input('ㆍ');

            // Assert
            Assert.Equal("규", result4.ComposingText);

            _output.WriteLine($"ㄱ + ㅡ + ㆍ + ㆍ = 규");
        }

        #endregion

        #region 실전 단어 입력 테스트

        [Fact]
        public void 단어입력_가()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act
            var r1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);

            var r2 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);

            var r3 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);

            var final = _ime.Commit();
            committed.Append(final.CommittedText);

            // Assert
            Assert.Equal("가", committed.ToString());

            _output.WriteLine($"입력 성공: {committed}");
        }

        [Fact]
        public void 단어입력_쿠()
        {
            // Arrange & Act - ㄱ + ㄱ(→ㅋ) + ㅡ + ㆍ (=ㅜ) = 쿠
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㄱ');  // ㅋ로 순환
            var r3 = _ime.Input('ㅡ');
            var r4 = _ime.Input('ㆍ');

            // Assert
            Assert.Equal("쿠", r4.ComposingText);

            _output.WriteLine($"조합중: {r4.ComposingText}");
        }

        [Fact]
        public void 단어입력_안녕()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act - "안" 입력: ㅇ + ㅣ + ㆍ (=ㅏ) + ㄴ
            var r1 = _ime.Input('ㅇ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);

            var r2 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);

            var r3 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);

            var r4 = _ime.Input('ㄴ');  // 종성
            if (!string.IsNullOrEmpty(r4.CommittedText)) committed.Append(r4.CommittedText);

            // 중간 확정 (Space 키 대신 Commit 사용)
            var commitResult = _ime.Commit();
            if (!string.IsNullOrEmpty(commitResult.CommittedText)) committed.Append(commitResult.CommittedText);

            // "녕" 입력: ㄴ + ㆍ + ㆍ + ㅣ (=ㅕ) + ㅇ
            var r5 = _ime.Input('ㄴ');  // 새 글자 시작
            if (!string.IsNullOrEmpty(r5.CommittedText)) committed.Append(r5.CommittedText);

            var r6 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r6.CommittedText)) committed.Append(r6.CommittedText);

            var r7 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r7.CommittedText)) committed.Append(r7.CommittedText);

            var r8 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r8.CommittedText)) committed.Append(r8.CommittedText);

            var r9 = _ime.Input('ㅇ');  // 종성
            if (!string.IsNullOrEmpty(r9.CommittedText)) committed.Append(r9.CommittedText);

            _output.WriteLine($"Committed: {r9.CommittedText}, Composing: {r9.ComposingText}");

            var final = _ime.Commit();
            committed.Append(final.CommittedText);

            // Assert
            Assert.Equal("안녕", committed.ToString());

            _output.WriteLine($"입력 성공: {committed}");
        }

        [Fact]
        public void 단어입력_한글()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act - "한" 입력: ㅅ + ㅅ(→ㅎ) + ㅣ + ㆍ(=ㅏ) + ㄴ
            var r1 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);

            var r2 = _ime.Input('ㅅ');  // ㅎ로 순환
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);

            var r3 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);

            var r4 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r4.CommittedText)) committed.Append(r4.CommittedText);

            var r5 = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(r5.CommittedText)) committed.Append(r5.CommittedText);

            // 중간 확정 (Commit 사용)
            var commitResult = _ime.Commit();
            if (!string.IsNullOrEmpty(commitResult.CommittedText)) committed.Append(commitResult.CommittedText);

            // "글" 입력: ㄱ + ㅡ + ㄴ + ㄴ(→ㄹ)
            var r6 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(r6.CommittedText)) committed.Append(r6.CommittedText);

            var r7 = _ime.Input('ㅡ');
            if (!string.IsNullOrEmpty(r7.CommittedText)) committed.Append(r7.CommittedText);

            var r8 = _ime.Input('ㄴ');
            if (!string.IsNullOrEmpty(r8.CommittedText)) committed.Append(r8.CommittedText);

            var r9 = _ime.Input('ㄴ');  // ㄹ로 순환
            if (!string.IsNullOrEmpty(r9.CommittedText)) committed.Append(r9.CommittedText);

            var final = _ime.Commit();
            committed.Append(final.CommittedText);

            // Assert
            Assert.Equal("한글", committed.ToString());

            _output.WriteLine($"입력 성공: {committed}");
        }

        [Fact]
        public void 단어입력_한글_중간확정없이()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act - "한글" 입력: ㅅㅅㅏㄴㄱㅡㄴㄴ (중간 확정 없이)
            // "한": ㅅ + ㅅ(→ㅎ) + ㅣ + ㆍ(=ㅏ) + ㄴ
            var r1 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);
            _output.WriteLine($"r1: composing={r1.ComposingText}, committed={r1.CommittedText}");

            var r2 = _ime.Input('ㅅ');  // ㅎ로 순환
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);
            _output.WriteLine($"r2: composing={r2.ComposingText}, committed={r2.CommittedText}");

            var r3 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);
            _output.WriteLine($"r3: composing={r3.ComposingText}, committed={r3.CommittedText}");

            var r4 = _ime.Input('ㆍ');  // ㅏ
            if (!string.IsNullOrEmpty(r4.CommittedText)) committed.Append(r4.CommittedText);
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");

            var r5 = _ime.Input('ㄴ');  // 종성
            if (!string.IsNullOrEmpty(r5.CommittedText)) committed.Append(r5.CommittedText);
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            Assert.Equal("한", r5.ComposingText);  // "한" 조합 중

            // "글": ㄱ + ㅡ + ㄴ + ㄴ(→ㄹ) - 새로운 글자 조합이 시작되면서 "한"은 자동으로 확정되어야 함.
            var r6 = _ime.Input('ㄱ');  // 새 음절 시작, "한"은 아직 복합 종성 조합중이므로 자동 확정되지 않음
            if (!string.IsNullOrEmpty(r6.CommittedText)) committed.Append(r6.CommittedText);
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");
            Assert.Equal("한ㄱ", r6.ComposingText);  // "한ㄱ" 조합 중

            var r7 = _ime.Input('ㅡ');
            if (!string.IsNullOrEmpty(r7.CommittedText)) committed.Append(r7.CommittedText);
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");
            Assert.Equal("한", r7.CommittedText);  // "한" 확정 (한ㄱ 대기중에 모음 결합으로 자동 확정됨)
            Assert.Equal("그", r7.ComposingText);  // "그" 조합 중

            var r8 = _ime.Input('ㄴ');  // 종성
            if (!string.IsNullOrEmpty(r8.CommittedText)) committed.Append(r8.CommittedText);
            _output.WriteLine($"r8: composing={r8.ComposingText}, committed={r8.CommittedText}");

            var r9 = _ime.Input('ㄴ');  // ㄹ로 순환
            if (!string.IsNullOrEmpty(r9.CommittedText)) committed.Append(r9.CommittedText);
            _output.WriteLine($"r9: composing={r9.ComposingText}, committed={r9.CommittedText}");
            Assert.Equal("글", r9.ComposingText);  // "글" 조합 중

            var final = _ime.Commit();
            committed.Append(final.CommittedText);

            // Assert
            Assert.Equal("한글", committed.ToString());

            _output.WriteLine($"입력 성공: {committed}");
        }

        #endregion

        #region 백스페이스 테스트

        [Fact]
        public void 백스페이스_모음조합_되돌리기()
        {
            // Act
            var r1 = _ime.Input('ㄱ');
            _output.WriteLine($"r1: composing={r1.ComposingText}, committed={r1.CommittedText}");
            var r2 = _ime.Input('ㅣ');
            _output.WriteLine($"r2: composing={r2.ComposingText}, committed={r2.CommittedText}");
            var r3 = _ime.Input('ㆍ');  // "가"
            _output.WriteLine($"r3: composing={r3.ComposingText}, committed={r3.CommittedText}");

            Assert.Equal("가", r3.ComposingText);

            var r4 = _ime.Backspace();  // 모음 한 단계 제거
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");

            // Assert
            Assert.Equal("ㄱ", r4.ComposingText);  // ㅏ가 제거되어 ㄱ만 남음

            _output.WriteLine($"백스페이스 후: {r4.ComposingText}");
        }

        [Fact]
        public void 백스페이스_초성_완전삭제()
        {
            // Act - ㄱㄱㄱ (ㄲ) -> backspace -> ""
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㄱ');  // ㅋ
            var r3 = _ime.Input('ㄱ');  // ㄲ

            Assert.Equal("ㄲ", r3.ComposingText);

            var r4 = _ime.Backspace();  // 초성 블록 삭제 -> ""

            // Assert
            Assert.Equal("", r4.ComposingText);

            _output.WriteLine($"백스페이스 후: 빈 문자열");
        }

        [Fact]
        public void 백스페이스_중성제거_후_초성만남음()
        {
            // Act - ㄱㄱㄱㅏ (까) -> backspace -> ㄲ
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㄱ');  // ㅋ
            var r3 = _ime.Input('ㄱ');  // ㄲ
            var r4 = _ime.Input('ㅣ');
            var r5 = _ime.Input('ㆍ');  // 까

            Assert.Equal("까", r5.ComposingText);

            var r6 = _ime.Backspace();  // 모음 제거

            // Assert
            Assert.Equal("ㄲ", r6.ComposingText);

            _output.WriteLine($"백스페이스 후: {r6.ComposingText}");
        }

        [Fact]
        public void 백스페이스_종성제거_후_초성중성만남음()
        {
            // Act - ㄱㄱㄱㅏㄱㄱㄱ (깤) -> backspace -> 까
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㄱ');  // ㅋ
            var r3 = _ime.Input('ㄱ');  // ㄲ
            var r4 = _ime.Input('ㅣ');
            var r5 = _ime.Input('ㆍ');  // 까
            var r6 = _ime.Input('ㄱ');  // 깍 (종성 ㄱ)
            var r7 = _ime.Input('ㄱ');  // 깎 (종성 ㅋ)
            var r8 = _ime.Input('ㄱ');  // 깤 (종성 ㄲ)

            Assert.Equal("깎", r8.ComposingText);

            var r9 = _ime.Backspace();  // 종성 블록 삭제

            // Assert
            Assert.Equal("까", r9.ComposingText);

            _output.WriteLine($"백스페이스 후: {r9.ComposingText}");
        }

        [Fact]
        public void 백스페이스_복합중성분해()
        {
            // Act - 와 -> backspace -> 오
            var r1 = _ime.Input('ㅇ');
            _output.WriteLine($"r1: composing={r1.ComposingText}, committed={r1.CommittedText}");
            var r2 = _ime.Input('ㆍ');
            _output.WriteLine($"r2: composing={r2.ComposingText}, committed={r2.CommittedText}");
            var r3 = _ime.Input('ㅡ');  // 오
            _output.WriteLine($"r3: composing={r3.ComposingText}, committed={r3.CommittedText}");
            var r4 = _ime.Input('ㅣ');
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");
            var r5 = _ime.Input('ㆍ');  // 와
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            Assert.Equal("와", r5.ComposingText);

            var r6 = _ime.Backspace();  // 복합 중성 분해
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");

            // Assert
            Assert.Equal("오", r6.ComposingText);

            _output.WriteLine($"백스페이스 후: {r6.ComposingText}");
        }

        [Fact]
        public void 백스페이스_복합모음후_자음만남음()
        {
            // Act - ㄱ + ㆍ + ㆍ + ㅣ (=ㅕ) 후 백스페이스
            var r1 = _ime.Input('ㄱ');
            _output.WriteLine($"r1: composing={r1.ComposingText}, committed={r1.CommittedText}");
            var r2 = _ime.Input('ㆍ');
            _output.WriteLine($"r2: composing={r2.ComposingText}, committed={r2.CommittedText}");
            var r3 = _ime.Input('ㆍ');
            _output.WriteLine($"r3: composing={r3.ComposingText}, committed={r3.CommittedText}");
            var r4 = _ime.Input('ㅣ');
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");

            Assert.Equal("겨", r4.ComposingText);

            var r5 = _ime.Backspace();  // 모음 전체 제거
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");

            // Assert
            Assert.Equal("ㄱ", r5.ComposingText);

            _output.WriteLine($"백스페이스 후: {r5.ComposingText}");
        }

        #endregion

        #region 고급 조합 테스트

        [Fact]
        public void 조합_모음완성후_새모음시작()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act - ㄱ + ㆍ + ㅡ (=ㅗ) → "고", Commit, 그 다음 ㅣ + ㆍ (=ㅏ) + ㆍ (=ㅑ) → "ㅑ" 조합중
            var r1 = _ime.Input('ㄱ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);

            var r2 = _ime.Input('ㆍ');
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);

            var r3 = _ime.Input('ㅡ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);
            Assert.Equal("고", r3.ComposingText);  // ㆍ + ㅡ = ㅗ

            // Commit 후 새 모음 조합 시작
            var commit1 = _ime.Commit();
            committed.Append(commit1.CommittedText);

            var r4 = _ime.Input('ㅣ');
            if (!string.IsNullOrEmpty(r4.CommittedText)) committed.Append(r4.CommittedText);

            var r5 = _ime.Input('ㆍ');  // ㅣ + ㆍ → ㅏ
            if (!string.IsNullOrEmpty(r5.CommittedText)) committed.Append(r5.CommittedText);
            Assert.Equal("ㅏ", r5.ComposingText);

            var r6 = _ime.Input('ㆍ');  // ㅏ + ㆍ → ㅑ
            if (!string.IsNullOrEmpty(r6.CommittedText)) committed.Append(r6.CommittedText);

            // Assert - "고"가 확정되고 "ㅑ"가 조합중
            Assert.Equal("고", committed.ToString());
            Assert.Equal("ㅑ", r6.ComposingText);

            _output.WriteLine($"완성: '{committed}', 조합중: '{r6.ComposingText}'");
        }

        [Fact]
        public void 조합_종성순환후_종성추가()
        {
            // Act - ㅅ + ㅅ(→ㅎ) + ㅡ + ㄴ + ㄴ(→ㄹ) + ㄱ = 흙
            var r1 = _ime.Input('ㅅ');
            var r2 = _ime.Input('ㅅ');  // ㅎ로 순환
            Assert.Equal("ㅎ", r2.ComposingText);

            var r3 = _ime.Input('ㅡ');
            Assert.Equal("흐", r3.ComposingText);

            var r4 = _ime.Input('ㄴ');  // 종성
            Assert.Equal("흔", r4.ComposingText);

            var r5 = _ime.Input('ㄴ');  // ㄹ로 순환
            Assert.Equal("흘", r5.ComposingText);

            var r6 = _ime.Input('ㄱ');  // 새 글자 시작 아니고 복합 종성

            // 복합 종성 ㄺ이 가능하므로 "흙"이 조합중
            Assert.Equal("흙", r6.ComposingText);

            _output.WriteLine($"조합중: {r6.ComposingText}");
        }

        [Fact]
        public void 조합_복합종성후_모음시작()
        {
            // Arrange
            var committed = new StringBuilder();

            // Act - ㅅ + ㅅ(→ㅎ) + ㅡ + ㄴ + ㄴ(→ㄹ) + ㄱ + ㅣ + ㆍ
            var r1 = _ime.Input('ㅅ');
            if (!string.IsNullOrEmpty(r1.CommittedText)) committed.Append(r1.CommittedText);

            var r2 = _ime.Input('ㅅ');  // ㅎ로 순환
            if (!string.IsNullOrEmpty(r2.CommittedText)) committed.Append(r2.CommittedText);

            var r3 = _ime.Input('ㅡ');
            if (!string.IsNullOrEmpty(r3.CommittedText)) committed.Append(r3.CommittedText);

            var r4 = _ime.Input('ㄴ');  // 종성
            if (!string.IsNullOrEmpty(r4.CommittedText)) committed.Append(r4.CommittedText);

            var r5 = _ime.Input('ㄴ');  // ㄹ로 순환
            if (!string.IsNullOrEmpty(r5.CommittedText)) committed.Append(r5.CommittedText);

            var r6 = _ime.Input('ㄱ');  // 복합 종성 ㄺ
            if (!string.IsNullOrEmpty(r6.CommittedText)) committed.Append(r6.CommittedText);
            Assert.Equal("흙", r6.ComposingText);

            var r7 = _ime.Input('ㅣ');  // 종성 분해: "흘" 확정, "ㄱ"이 초성으로 "ㅣ"가 중성으로 조합
            if (!string.IsNullOrEmpty(r7.CommittedText)) committed.Append(r7.CommittedText);

            var r8 = _ime.Input('ㆍ');  // ㅣ + ㆍ = ㅏ
            if (!string.IsNullOrEmpty(r8.CommittedText)) committed.Append(r8.CommittedText);

            // Assert - "흘"이 확정되고 "가"가 조합중
            Assert.Equal("흘", committed.ToString());
            Assert.Equal("가", r8.ComposingText);

            _output.WriteLine($"완성: '{committed}', 조합중: '{r8.ComposingText}'");
        }

        [Fact]
        public void 복합종성후_종성순환_테스트()
        {
            // Act - ㅅ + ㅅ(→ㅎ) + ㅡ + ㄴ + ㄴ(→ㄹ) + ㄱ + ㄱ + ㄱ 입력시 처리 확인
            var r1 = _ime.Input('ㅅ');
            var r2 = _ime.Input('ㅅ');
            var r3 = _ime.Input('ㅡ');
            var r4 = _ime.Input('ㄴ');
            var r5 = _ime.Input('ㄴ');
            var r6 = _ime.Input('ㄱ'); // 흙
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");

            var r7 = _ime.Input('ㄱ'); // 흘(ㅋ)
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");

            var r8 = _ime.Input('ㄱ'); // 흘(ㄲ)
            _output.WriteLine($"r8: composing={r8.ComposingText}, committed={r8.CommittedText}");

            var r9 = _ime.Input('ㄱ'); // 흙
            _output.WriteLine($"r9: composing={r9.ComposingText}, committed={r9.CommittedText}");

            var r10 = _ime.Input('ㄱ'); // 흘ㅋ(조합중)
            _output.WriteLine($"r10: composing={r10.ComposingText}, committed={r10.CommittedText}");

            var r11 = _ime.Input('ㅣ'); // 흘(완료) 키(조합중)
            _output.WriteLine($"r11: composing={r11.ComposingText}, committed={r11.CommittedText}");

            var r12 = _ime.Input('ㆍ'); // 카(조합중)
            _output.WriteLine($"r12: composing={r12.ComposingText}, committed={r12.CommittedText}");

            // Assert - 테스트 결과 "흙 - 흘(ㅋ) - 흘(ㄲ) - 흙"이 되어야 함 
            Assert.Equal("흙", r6.ComposingText);
            Assert.Equal("흘ㅋ", r7.ComposingText);
            Assert.Equal("흘ㄲ", r8.ComposingText);
            Assert.Equal("흙", r9.ComposingText);
            Assert.Equal("흘ㅋ", r10.ComposingText);

            Assert.Equal("흘", r11.CommittedText);
            Assert.Equal("키", r11.ComposingText);
            Assert.Equal("카", r12.ComposingText);
        }

        [Fact]
        public void 복합종성후_종성순환_테스트_ㄼ_ㄿ_ㅂ_조합중()
        {
            // Act - ㅅ + ㅅ(→ㅎ) + ㅡ + ㄴ + ㄴ(→ㄹ) + ㄱ + ㄼ + ㄿ + ㅂ
            var r1 = _ime.Input('ㅅ');
            var r2 = _ime.Input('ㅅ');
            var r3 = _ime.Input('ㅡ');
            var r4 = _ime.Input('ㄴ');
            var r5 = _ime.Input('ㄴ');
            var r6 = _ime.Input('ㅂ'); // 흛
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");

            var r7 = _ime.Input('ㅂ'); // 흞
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");

            var r8 = _ime.Input('ㅂ'); // 흘ㅃ(조합중)
            _output.WriteLine($"r8: composing={r8.ComposingText}, committed={r8.CommittedText}");

            // Assert - 흛 - 흞 - 흘ㅃ(조합중)
            Assert.Equal("흛", r6.ComposingText);
            Assert.Equal("흞", r7.ComposingText);
            Assert.Equal("흘ㅃ", r8.ComposingText);
        }

        [Fact]
        public void 복합종성_조합_가능성_대기_테스트_안ㅅ_않_안ㅆ()
        {
            // Act - 안 + ㅅ + 않 + 안 + ㅆ
            var r1 = _ime.Input('ㅇ');
            var r2 = _ime.Input('ㅣ');
            var r3 = _ime.Input('ㆍ');
            var r4 = _ime.Input('ㄴ'); // 안
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");
            var r5 = _ime.Input('ㅅ'); // 안ㅅ
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            var r6 = _ime.Input('ㅅ'); // 않
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");
            var r7 = _ime.Input('ㅅ'); // 안ㅆ
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");

            // Assert - 안ㅅ - 않 - 안ㅆ(조합중)
            Assert.Equal("안", r4.ComposingText);
            Assert.Equal("안ㅅ", r5.ComposingText);
            Assert.Equal("않", r6.ComposingText);
            Assert.Equal("안ㅆ", r7.ComposingText);
        }

        [Fact]
        public void 복합종성_조합_가능성_조합완료_테스트_안ㅅ_않_안ㅆ_안쓰()
        {
            // Act - 안 + ㅅ + 않 + 안 + ㅆ
            var r1 = _ime.Input('ㅇ');
            var r2 = _ime.Input('ㅣ');
            var r3 = _ime.Input('ㆍ');
            var r4 = _ime.Input('ㄴ'); // 안
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");
            var r5 = _ime.Input('ㅅ'); // 안ㅅ
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            var r6 = _ime.Input('ㅅ'); // 않
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");
            var r7 = _ime.Input('ㅅ'); // 안ㅆ
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");
            var r8 = _ime.Input('ㅡ'); // 안쓰
            _output.WriteLine($"r8: composing={r8.ComposingText}, committed={r8.CommittedText}");

            // Assert - 안ㅅ - 않 - 안ㅆ - 안(완료) 쓰(조합중)
            Assert.Equal("안ㅅ", r5.ComposingText);
            Assert.Equal("않", r6.ComposingText);
            Assert.Equal("안ㅆ", r7.ComposingText);

            Assert.Equal("안", r8.CommittedText);
            Assert.Equal("쓰", r8.ComposingText);
        }

        #endregion

        #region 복합 모음 분해 및 재조합 테스트 (천지인 특수)

        [Fact]
        public void 복합모음분해_ㄱㆍㅡㅣㆍㆍ_고확정_ㅑ조합중()
        {
            // Act - ㄱ + ㆍ + ㅡ + ㅣ + ㆍ + ㆍ
            // 예상: ㄱ + (ㆍ + ㅡ = ㅗ) -> 고
            //      고 + ㅣ -> 괴 (ㅗ + ㅣ = ㅚ)
            //      괴 + ㆍ -> 조합 실패, 복합 중성 분해: ㅚ = ㅗ + ㅣ
            //                ㅣ + ㆍ = ㅏ 조합 가능 -> "고" 확정, "ㅏ" 조합중
            //      ㅏ + ㆍ -> "ㅑ"
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㆍ');
            var r3 = _ime.Input('ㅡ');  // ㆍ + ㅡ -> ㅗ, 고
            Assert.Equal("고", r3.ComposingText);

            var r4 = _ime.Input('ㅣ');  // 고 + ㅣ -> 괴 (ㅚ)
            Assert.Equal("괴", r4.ComposingText);

            var r5 = _ime.Input('ㆍ');  // 괴 + ㆍ -> 과 (ㅚ + ㆍ = ㅘ)
            Assert.Equal("과", r5.ComposingText);
            _output.WriteLine($"r5: {r5.ComposingText}");

            var r6 = _ime.Input('ㆍ');  // 과 + ㆍ -> 조합 실패, 복합 중성 분해: ㅘ = ㅗ + ㅏ
                                       // ㅏ + ㆍ = ㅑ 조합 가능 -> "고" 확정, "ㅑ" 조합중
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");

            // Assert
            Assert.Equal("고", r6.CommittedText);  // r6에서 "고" 확정
            Assert.Equal("ㅑ", r6.ComposingText);   // r6에서 "ㅑ" 조합중

            _output.WriteLine($"결과: 확정={r5.CommittedText}, 조합중={r6.ComposingText}");
        }

        [Fact]
        public void 복합모음분해_ㄱㅡㆍㆍㅣㆍ_궈확정_ㆍ조합중()
        {
            // Act - ㄱ + ㅡ + ㆍ + ㆍ + ㅣ + ㆍ
            // 예상: 구 -> 규 -> 궈 (ㅝ)
            //      궈 + ㆍ -> 조합 실패, 복합 중성 분해: ㅝ = ㅜ + ㅓ
            //                ㅓ + ㆍ 조합 불가 -> "궈" 확정, "ㆍ" 조합중
            var r1 = _ime.Input('ㄱ');
            _output.WriteLine($"r1: composing={r1.ComposingText}, committed={r1.CommittedText}");
            var r2 = _ime.Input('ㅡ');  // 그
            _output.WriteLine($"r2: composing={r2.ComposingText}, committed={r2.CommittedText}");
            var r3 = _ime.Input('ㆍ');  // 구 (ㅜ)
            _output.WriteLine($"r3: composing={r3.ComposingText}, committed={r3.CommittedText}");
            Assert.Equal("구", r3.ComposingText);

            var r4 = _ime.Input('ㆍ');  // 규 (ㅠ)
            _output.WriteLine($"r4: composing={r4.ComposingText}, committed={r4.CommittedText}");
            Assert.Equal("규", r4.ComposingText);

            var r5 = _ime.Input('ㅣ');  // 궈 (ㅝ)
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            Assert.Equal("궈", r5.ComposingText);

            var r6 = _ime.Input('ㆍ');  // 조합 실패 -> 복합 중성 분해 시도
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");

            var r7 = _ime.Input('ㆍ');  // 조합 대기
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");

            var r8 = _ime.Input('ㅣ');  // ㅕ
            _output.WriteLine($"r8: composing={r8.ComposingText}, committed={r8.CommittedText}");

            // Assert
            Assert.Equal("궈", r6.CommittedText);  // "궈" 확정
            Assert.Equal("ㆍ", r6.ComposingText);  // "ㆍ" 조합중
            Assert.Equal("ㆍㆍ", r7.ComposingText); // "ㆍㆍ" 조합중
            Assert.Equal("ㅕ", r8.ComposingText); // "ㅕ" 조합중
        }

        [Fact]
        public void 백스페이스_복합중성분해_궤백스페이스_구조합중()
        {
            // Act - ㄱ + ㅡ + ㆍ + ㆍ + ㅣ + ㅣ + backspace
            // 예상: 구 -> 규 -> 궈 (ㅝ) -> 궤 (ㅞ)
            //      백스페이스 -> 복합 중성 분해: ㅞ = ㅜ + ㅔ -> 구
            var r1 = _ime.Input('ㄱ');
            var r2 = _ime.Input('ㅡ');  // 그
            var r3 = _ime.Input('ㆍ');  // 구 (ㅜ)
            var r4 = _ime.Input('ㆍ');  // 규 (ㅠ)
            var r5 = _ime.Input('ㅣ');  // 궈 (ㅝ)
            _output.WriteLine($"r5: composing={r5.ComposingText}, committed={r5.CommittedText}");
            Assert.Equal("궈", r5.ComposingText);

            var r6 = _ime.Input('ㅣ');  // 궤 (ㅞ)
            _output.WriteLine($"r6: composing={r6.ComposingText}, committed={r6.CommittedText}");
            Assert.Equal("궤", r6.ComposingText);

            var r7 = _ime.Backspace();  // 복합 중성 분해: ㅞ = ㅜ + ㅔ -> 구
            _output.WriteLine($"r7: composing={r7.ComposingText}, committed={r7.CommittedText}");

            // Assert
            Assert.Equal("구", r7.ComposingText);

            _output.WriteLine($"백스페이스 후: {r7.ComposingText}");
        }

        #endregion
    }
}

