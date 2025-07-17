// cookieUtil.js

/**
 * 设置一个 Cookie，包含指定的键、值和可选的过期天数。
 * @param {string} key Cookie 的名称。
 * @param {string} value Cookie 的值。
 * @param {number} [days] Cookie 过期的天数。如果未提供，它将是一个会话 Cookie。
 * @param {string} [path='/'] Cookie 有效的路径。默认为 '/'。
 */
export function setCookie(key, value, days, path = '/') {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    // 正确设置或更新 Cookie 的方式。浏览器会处理查找/创建它。
    // 确保设置 path，通常设置为 "/" 以便在整个站点范围内可用。
    document.cookie = key + "=" + encodeURIComponent(value) + expires + "; path=" + path +"; SameSite=Strict";
}

/**
 * 根据键获取 Cookie 的值。
 * @param {string} key 要检索的 Cookie 的名称。
 * @returns {string} Cookie 的解码值，如果未找到则返回空字符串。
 */
export function getCookie(key) {
    // 按分号和空格分割 Cookie，然后修剪每个部分以处理前导/尾随空格
    const cookies = document.cookie.split('; ').map(c => c.trim());
    let value = '';

    for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i];
        // 检查 Cookie 是否以键后跟等号开头
        if (cookie.startsWith(key + '=')) {
            // 提取值部分并解码
            value = decodeURIComponent(cookie.substring(key.length + 1));
            break; // 找到 Cookie，退出循环
        }
    }
    return value;
}

// 可选：一个删除 Cookie 的函数
/**
 * 通过将其过期日期设置为过去来删除 Cookie。
 * @param {string} key 要删除的 Cookie 的名称。
 * @param {string} [path='/'] 要删除的 Cookie 的路径。必须与设置时的路径匹配。
 */
export function deleteCookie(key, path = '/') {
    // 将 Cookie 的过期日期设置为过去，以删除它
    document.cookie = key + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=" + path;
}